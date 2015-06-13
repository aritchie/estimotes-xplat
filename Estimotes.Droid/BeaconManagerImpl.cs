using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using EstimoteSdk;


namespace Estimotes {

    public class BeaconManagerImpl : AbstractBeaconManagerImpl {

        private readonly BeaconManager beaconManager;
		private readonly object syncLock = new object();
        private bool? isAvailable;


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
                var beacons = args.Beacons.Select(x => {
                    var prox = this.FromNative(Utils.ComputeProximity(x));
                    var beacon = new Beacon(
						args.Region.ProximityUUID,
						args.Region.Identifier, 
						prox, 
						(ushort)x.Minor, 
						(ushort)x.Major
					);
                    return beacon;
                });
                this.OnRanged(beacons);
            };
        }


        public override async Task<bool> IsAvailable() {
            if (this.isAvailable != null)
                return this.isAvailable.Value;

			if (!this.beaconManager.CheckPermissionsAndService())
				return false;
			
            var tcs = new TaskCompletionSource<object>();
			lock (this.syncLock) {
				if (this.isAvailable != null)
					tcs.TrySetResult(null);
				
				//Application.Context.StartService(new Intent(Application.Context, typeof(EstimoteSdk.Connection.BeaconService)));
				var ready = new ServiceReadyCallbackImpl(() => tcs.TrySetResult(null));
				this.beaconManager.Connect(ready);
				this.isAvailable = true;
			}

            await tcs.Task;
            return this.isAvailable.Value;
        }


        public override void StartMonitoring(params BeaconRegion[] regions) {
            foreach (var region in regions) {
                var native = this.ToNative(region);
                this.beaconManager.StartMonitoring(native);
            }
        }


        public override void StartRanging(params BeaconRegion[] regions) {
            foreach (var region in regions) {
                var native = this.ToNative(region);
                this.beaconManager.StartRanging(native);
            }
        }


        public override void StopMonitoring(params BeaconRegion[] regions) {
            foreach (var region in regions) {
                var native = this.ToNative(region);
                this.beaconManager.StopMonitoring(native);
            }
        }


        public override void StopRanging(params BeaconRegion[] regions) {
            foreach (var region in regions) {
                var native = this.ToNative(region);
                this.beaconManager.StopRanging(native);
            }
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


        protected virtual BeaconRegion FromNative(EstimoteSdk.Region native) {
            // TODO: minor & major?
            return new BeaconRegion(native.ProximityUUID, native.Identifier);
        }


        protected virtual EstimoteSdk.Region ToNative(BeaconRegion region) {
            var native = new Region(region.Identifier, region.Uuid, null, null);
            return native;
        }
    }
}