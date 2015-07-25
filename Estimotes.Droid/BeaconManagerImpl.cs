using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using EstimoteSdk;
using Java.Lang;


namespace Estimotes {

    public class BeaconManagerImpl : AbstractBeaconManagerImpl {

        private readonly BeaconManager beaconManager;
		readonly object syncLock = new object();
        bool isConnected;


        public BeaconManagerImpl() {
            this.beaconManager = new BeaconManager(Application.Context);
            this.beaconManager.Nearable += (sender, args) => {
                var nearables = args.P0.Select(x => new Nearable(x));
                this.OnNearables(nearables);
            };
            //this.beaconManager.Eddystone += (sender, args) => { };

            this.beaconManager.EnteredRegion += (sender, args) => {
                var region = this.FromNative(args.Region);
                this.OnRegionStatusChanged(region, true);
            };
            this.beaconManager.ExitedRegion += (sender, args) => {
                var region = this.FromNative(args.Region);
                this.OnRegionStatusChanged(region, false);
            };
            this.beaconManager.Ranging += (sender, args) => {
				var beacons = args.Beacons.Select(x => new Beacon(args.Region, x));
                this.OnRanged(beacons);
            };
        }


        public override async Task<BeaconInitStatus> Initialize() {
            if (this.isConnected)
                return BeaconInitStatus.Success;

            if (!this.beaconManager.HasBluetooth)
                return BeaconInitStatus.BluetoothMissing;

            if (!this.beaconManager.IsBluetoothEnabled)
				return BeaconInitStatus.BluetoothOff;

			if (!this.beaconManager.CheckPermissionsAndService())
				return BeaconInitStatus.PermissionDenied;

            var tcs = new TaskCompletionSource<BeaconInitStatus>();
			lock (this.syncLock) {
				if (this.isConnected)
					tcs.TrySetResult(BeaconInitStatus.Success);

				//Application.Context.StartService(new Intent(Application.Context, typeof(EstimoteSdk.Connection.BeaconService)));
				var ready = new ServiceReadyCallbackImpl(() => {
				    this.isConnected = true;
                    tcs.TrySetResult(BeaconInitStatus.Success);
				});
				this.beaconManager.Connect(ready);
			}

            var status = await tcs.Task;
            // restore monitored beacons
            if (status == BeaconInitStatus.Success)
                foreach (var region in this.MonitoringRegions)
                    this.StartMonitoringNative(region);

            return status;
        }


        public override string StartNearableDiscovery() {
            return this.beaconManager.StartNearableDiscovery();
        }


        public override void StopNearableDiscovery(string id) {
            this.beaconManager.StopNearableDiscovery(id);
        }

        protected override void StartMonitoringNative(BeaconRegion region) {
			var native = this.ToNative(region);
			this.beaconManager.StartMonitoring(native);
        }


		protected override void StopMonitoringNative(BeaconRegion region) {
			var native = this.ToNative(region);
			this.beaconManager.StopMonitoring(native);
        }


        protected override void StartRangingNative(BeaconRegion region) {
            var native = this.ToNative(region);
            this.beaconManager.StartRanging(native);
        }


        protected override void StopRangingNative(BeaconRegion region) {
            var native = this.ToNative(region);
            this.beaconManager.StopRanging(native);
        }


        protected virtual BeaconRegion FromNative(Region native) {
            return new BeaconRegion(
                native.Identifier,
                native.ProximityUUID,
				(ushort)native.Major.IntValue(),
				(ushort)native.Minor.IntValue()
            );
        }


        protected virtual Region ToNative(BeaconRegion region) {
			return new Region(
				region.Identifier,
				region.Uuid,
				this.ToInteger(region.Major),
				this.ToInteger(region.Minor)
			);
        }


		protected virtual Integer ToInteger(ushort? num) {
			if (num == null || num == 0)
				return null;

			return new Integer(num.Value);
		}
    }
}