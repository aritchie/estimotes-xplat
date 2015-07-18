using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using EstimoteSdk;
using Java.Lang;


namespace Estimotes {

    public class BeaconManagerImpl : AbstractBeaconManagerImpl {

        private readonly BeaconManager beaconManager;
		private readonly object syncLock = new object();
        private bool isConnected;


        public BeaconManagerImpl() {
            this.beaconManager = new BeaconManager(Application.Context);
            this.beaconManager.EnteredRegion += (sender, args) => {
                var region = this.FromNative(args.Region);
                this.OnEnteredRegion(region);
            };
            this.beaconManager.ExitedRegion += (sender, args) => {
                var region = this.FromNative(args.Region);
                this.OnExitedRegion(region);
            };
            this.beaconManager.Ranging += (sender, args) => {
                var beacons = args.Beacons.Select(this.FromNative);
                this.OnRanged(beacons);
            };
        }


        public override async Task<bool> Initialize() {
            if (this.isConnected)
                return true;

			if (!this.beaconManager.CheckPermissionsAndService())
				return false;

            var tcs = new TaskCompletionSource<object>();
			lock (this.syncLock) {
				if (this.isConnected)
					tcs.TrySetResult(null);

				//Application.Context.StartService(new Intent(Application.Context, typeof(EstimoteSdk.Connection.BeaconService)));
				var ready = new ServiceReadyCallbackImpl(() => {
				    this.isConnected = true;
                    tcs.TrySetResult(null);
				});
				this.beaconManager.Connect(ready);
			}

            await tcs.Task;
            return this.isConnected;
        }


        public override void StartMonitoring(string uuid, ushort? major = null, ushort? minor = null) {
            this.beaconManager.StartMonitoring(new Region(null, uuid, new Integer(major ?? 0), new Integer(minor ?? 0)));
        }


        public override void StopMonitoring(string uuid, ushort? major = null, ushort? minor = null) {
            this.beaconManager.StopMonitoring(new Region(null, uuid, new Integer(major ?? 0), new Integer(minor ?? 0)));
        }


        public override void StartRanging(BeaconRegion region) {
            var native = this.ToNative(region);
            this.beaconManager.StartRanging(native);
        }


        public override void StopRanging(BeaconRegion region) {
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


        protected virtual Beacon FromNative(EstimoteSdk.Beacon native) {
            var prox = this.FromNative(Utils.ComputeProximity(native));
            var beacon = new Beacon(
                native.ProximityUUID,
                native.Name,
                prox,
				(ushort)native.Minor,
				(ushort)native.Major
			);
            return beacon;
        }

        protected virtual BeaconRegion FromNative(Region native) {
            return new BeaconRegion(
                native.Identifier,
                native.ProximityUUID,
                this.JavaToNumber(native.Major),
                this.JavaToNumber(native.Minor)
            );
        }


        protected virtual Region ToNative(BeaconRegion region) {
            var native = new Region(region.Identifier, region.Uuid, null, null);
            return native;
        }


        protected virtual ushort? JavaToNumber(Integer integer) {
            if (integer == null || integer.IntValue() == 0)
                return null;

            return (ushort)integer.IntValue();
        }
    }
}