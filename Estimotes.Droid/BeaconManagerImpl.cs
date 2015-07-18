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
				var beacons = args.Beacons.Select(x => this.FromNative(args.Region, x));
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


		public override void StartMonitoring(BeaconRegion region) {
			var native = this.ToNative(region);
			this.beaconManager.StartMonitoring(native);
			base.StartMonitoring(region);
        }


		public override void StopMonitoring(BeaconRegion region) {
			var native = this.ToNative(region);
			this.beaconManager.StopMonitoring(native);
			base.StopMonitoring(region);
        }


        public override void StartRanging(BeaconRegion region) {
            var native = this.ToNative(region);
            this.beaconManager.StartRanging(native);
			base.StartRanging(region);
        }


        public override void StopRanging(BeaconRegion region) {
            var native = this.ToNative(region);
            this.beaconManager.StopRanging(native);
			base.StopRanging(region);
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
				region.Identifier,
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