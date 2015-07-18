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
						var beacon = new Beacon(
							x.ProximityUUID.AsString(),
							x.Name,
							prox,
							x.Minor,
							x.Major
						);
                    	return beacon;
                	})
					.ToList();

				Debug.WriteLine("Beacons Ranged: " + beacons.Count);
				this.OnRanged(beacons);
            };
        }



        public override async Task<bool> Initialize() {
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


        public override void StartMonitoring(string uuid, ushort? major = null, ushort? minor = null) {
            var native = this.ToNative(new BeaconRegion(null, uuid, major, minor));
            this.beaconManager.StartMonitoring(native);
        }


        public override void StartRanging(BeaconRegion region) {
            var native = this.ToNative(region);
            this.beaconManager.StartRangingBeacons(native);
        }


        public override void StopMonitoring(string uuid, ushort? major = null, ushort? minor = null) {
            var native = this.ToNative(new BeaconRegion(null, uuid, major, minor));
            this.beaconManager.StopMonitoring(native);
        }


        public override void StopRanging(BeaconRegion region) {
            var native = this.ToNative(region);
            this.beaconManager.StopRangingBeacons(native);
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
            return new BeaconRegion(
                native.ProximityUuid.AsString(),
                native.Identifier,
                this.NSNumberToNumber(native.Major),
                this.NSNumberToNumber(native.Minor)
            );
        }


        protected virtual Estimote.BeaconRegion ToNative(BeaconRegion region) {
            var uuid = new NSUuid(region.Uuid);
            var native = new Estimote.BeaconRegion(uuid, region.Identifier);
            return native;
        }


        protected virtual ushort? NSNumberToNumber(NSNumber num) {
            if (num == null || num.UInt16Value == 0)
                return null;

            return num.UInt16Value;
        }
    }
}