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
                var region = this.FromNative(args.Region);
                this.OnEnteredRegion(region);
            };
            this.beaconManager.ExitedRegion += (sender, args) => {
				var region = this.FromNative(args.Region);
                this.OnExitedRegion(region);
            };
			this.beaconManager.RangedBeacons += (sender, args) => {
				var beacons = args.Beacons.Select(this.FromNative);
				this.OnRanged(beacons);
            };
        }



        public override async Task<bool> Initialize() {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(7, 0))
                return false;

			var good = false;
			var authStatus = BeaconManager.AuthorizationStatus();

			if (authStatus != CLAuthorizationStatus.NotDetermined)
				good = this.IsGoodStatus(authStatus);

			else {
				var tcs = new TaskCompletionSource<bool>();
				var funcPnt = new EventHandler<AuthorizationStatusChangedArgsEventArgs>((sender, args) => {
					if (args.Status == CLAuthorizationStatus.NotDetermined)
						return; // not done yet
					
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


		public override void StartMonitoring(BeaconRegion region) {
			var native = this.ToNative(region);
            this.beaconManager.StartMonitoring(native);
			base.StartMonitoring(region);
        }


        public override void StartRanging(BeaconRegion region) {
            var native = this.ToNative(region);
            this.beaconManager.StartRangingBeacons(native);
			base.StartRanging(region);
        }


		public override void StopMonitoring(BeaconRegion region) {
			var native = this.ToNative(region);
            this.beaconManager.StopMonitoring(native);
			base.StopMonitoring(region);
        }


        public override void StopRanging(BeaconRegion region) {
            var native = this.ToNative(region);
            this.beaconManager.StopRangingBeacons(native);
			base.StopRanging(region);
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


		protected virtual Beacon FromNative(Estimote.Beacon native) {
			var prox = this.FromNative(native.Proximity);
			var beacon = new Beacon(
				native.ProximityUUID.AsString(),
				String.Empty,
				String.Empty,
//				native.Name,
//				region.Identifier,
				prox,
				native.Minor,
				native.Major
			);
			return beacon;
		}


		protected virtual BeaconRegion FromNative(Estimote.BeaconRegion native) {
            return new BeaconRegion(
				native.Identifier,
				native.ProximityUuid.AsString(),
				native.Major.UInt16Value,
				native.Minor.UInt16Value
        	);
        }


        protected virtual Estimote.BeaconRegion ToNative(BeaconRegion region) {
			var uuid = new NSUuid(region.Uuid);
			if (region.Minor != null)
				return new Estimote.BeaconRegion(uuid, region.Major.Value, region.Minor.Value, region.Identifier);

			if (region.Major != null)
				return new Estimote.BeaconRegion(uuid, region.Major.Value, region.Identifier);

			return new Estimote.BeaconRegion(uuid, region.Identifier);
        }
    }
}