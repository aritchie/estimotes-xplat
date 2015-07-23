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
            this.beaconManager.EnteredRegion += (sender, args) => {
                var region = this.FromNative(args.Region);
                this.OnRegionStatusChanged(region, true);
            };
            this.beaconManager.ExitedRegion += (sender, args) => {
                var region = this.FromNative(args.Region);
                this.OnRegionStatusChanged(region, false);
            };
            this.beaconManager.Ranging += (sender, args) => {
				var beacons = args.Beacons.Select(x => this.FromNative(args.Region, x));
                this.OnRanged(beacons);
            };
        }


        public override async Task<BeaconInitStatus> Initialize() {
            if (this.isConnected)
                return BeaconInitStatus.Success;

			if (this.beaconManager.IsBluetoothEnabled)
				return BeaconInitStatus.BluetoothOff;
			
			if (!this.beaconManager.CheckPermissionsAndService())
				return BeaconInitStatus.PermissionDenied; // TODO: more!

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

            return await tcs.Task;
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


        protected virtual Proximity FromNative(Utils.Proximity prox) {
            if (prox == Utils.Proximity.Far)
                return Proximity.Far;

            if (prox == Utils.Proximity.Immediate)
                return Proximity.Immediate;

            if (prox == Utils.Proximity.Near)
                return Proximity.Near;

            return Proximity.Unknown;
        }


        protected virtual Beacon FromNative(EstimoteSdk.Region region, EstimoteSdk.Beacon native) {
            var prox = this.FromNative(Utils.ComputeProximity(native));
            var beacon = new Beacon(
                native.ProximityUUID,
                prox,
				(ushort)native.Major,
				(ushort)native.Minor
			);
            return beacon;
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