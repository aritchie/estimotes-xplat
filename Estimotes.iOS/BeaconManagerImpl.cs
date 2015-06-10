using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using CoreLocation;
using Foundation;
using UIKit;
using Estimote;


namespace Estimotes {

    public class BeaconManagerImpl : AbstractBeaconManagerImpl {
        private readonly BeaconManager beaconManager;


        public BeaconManagerImpl() {
            this.beaconManager = new BeaconManager {
                ReturnAllRangedBeaconsAtOnce = true
            };

            this.beaconManager.EnteredRegion += (sender, args) => {
				Debug.WriteLine("Entering Region: " + args.Region.Identifier);
                var region = this.FromNative(args.Region);
                this.OnEnteredRegion(region);
            };
            this.beaconManager.ExitedRegion += (sender, args) => {
				Debug.WriteLine("Exiting Region: " + args.Region.Identifier);
				var region = this.FromNative(args.Region);
                this.OnExitedRegion(region);
            };
            this.beaconManager.RangedBeacons += (sender, args) => {
                var beacons = args.Beacons
					.Select(x => {
                    	var prox = this.FromNative(x.Proximity);
						var region = new BeaconRegion(x.ProximityUUID.AsString(), x.Name);
						var beacon = new Beacon(region, prox, x.Minor, x.Major);
                    	return beacon;
                	})
					.ToList();
				
				Debug.WriteLine("Beacons Ranged: " + beacons.Count);
				this.OnRanged(beacons);
            };
        }



        public override async Task<bool> IsAvailable() {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                return true;

			var good = false;
			var authStatus = BeaconManager.AuthorizationStatus();

			if (authStatus != CLAuthorizationStatus.NotDetermined)
				good = this.IsGoodStatus(authStatus);
			
			else {
				var tcs = new TaskCompletionSource<bool>();
				var funcPnt = new EventHandler<AuthorizationStatusChangedArgsEventArgs>((sender, args) => {
					Console.WriteLine("[BeaconManager Authorization Status]: {0}", args.Status.ToString());
					var status = this.IsGoodStatus(args.Status);
					tcs.TrySetResult(status);
				});
				this.beaconManager.AuthorizationStatusChanged += funcPnt;
				this.beaconManager.RequestAlwaysAuthorization();
				good = await tcs.Task;
				this.beaconManager.AuthorizationStatusChanged -= funcPnt;
			}
			return good;
        }


		private bool IsGoodStatus(CLAuthorizationStatus status) {
			return (
			    status == CLAuthorizationStatus.Authorized ||
			    status == CLAuthorizationStatus.AuthorizedAlways ||
			    status == CLAuthorizationStatus.AuthorizedWhenInUse
			);
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
                this.beaconManager.StartRangingBeacons(native);
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
                this.beaconManager.StopRangingBeacons(native);
            }
        }


        protected virtual Proximity FromNative(CLProximity proximity) {
            switch (proximity) {

                case CLProximity.Far:
                    return Proximity.Far;

                case CLProximity.Immediate:
                    return Proximity.Immediate;

                case CLProximity.Near:
                    return Proximity.Near;

                default:
                    return Proximity.Unknown;
            }
        }


        protected virtual BeaconRegion FromNative(Estimote.BeaconRegion native) {
            // TODO: minor & major?
            return new BeaconRegion(native.ProximityUuid.AsString(), native.Identifier);
        }


        protected virtual Estimote.BeaconRegion ToNative(BeaconRegion region) {
            var uuid = new NSUuid(region.Uuid);
            var native = new Estimote.BeaconRegion(uuid, region.Identifier);
            return native;
        }
    }
}