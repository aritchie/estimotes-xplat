using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLocation;
using Foundation;
using UIKit;
using Estimote;


namespace Estimotes {

    public class BeaconManagerImpl : AbstractBeaconManagerImpl {
        readonly BeaconManager beaconManager;


        public BeaconManagerImpl() {
            this.beaconManager = new BeaconManager {
                ReturnAllRangedBeaconsAtOnce = true
            };
            this.beaconManager.EnteredRegion += (sender, args) => {
                var region = this.FromNative(args.Region);
                this.OnRegionStatusChanged(region, true);
            };
            this.beaconManager.ExitedRegion += (sender, args) => {
				var region = this.FromNative(args.Region);
                this.OnRegionStatusChanged(region, false);
            };
			this.beaconManager.RangedBeacons += (sender, args) => {
				var beacons = args.Beacons.Select(this.FromNative);
				this.OnRanged(beacons);
            };
        }



        public override async Task<BeaconInitStatus> Initialize() {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(7, 0))
                return BeaconInitStatus.InvalidOperatingSystem;

            // TODO: bluetooth enabled & present

			var authStatus = BeaconManager.AuthorizationStatus();
            var good = this.IsGoodStatus(authStatus);
            if (good)
                return BeaconInitStatus.Success;

			var tcs = new TaskCompletionSource<BeaconInitStatus>();
			var funcPnt = new EventHandler<AuthorizationStatusChangedArgsEventArgs>((sender, args) => {
				if (args.Status == CLAuthorizationStatus.NotDetermined)
					return; // not done yet

				var success = this.IsGoodStatus(args.Status);
                tcs.TrySetResult(success ? BeaconInitStatus.Success : BeaconInitStatus.PermissionDenied);
			});
			this.beaconManager.AuthorizationStatusChanged += funcPnt;
			this.beaconManager.RequestAlwaysAuthorization();
			var status = await tcs.Task;
			this.beaconManager.AuthorizationStatusChanged -= funcPnt;

            return status;
        }


		protected virtual bool IsGoodStatus(CLAuthorizationStatus status) {
			return (
			    status == CLAuthorizationStatus.Authorized ||
			    status == CLAuthorizationStatus.AuthorizedAlways ||
			    status == CLAuthorizationStatus.AuthorizedWhenInUse
			);
		}


		protected override void StartMonitoringNative(BeaconRegion region) {
			var native = this.ToNative(region);
            this.beaconManager.StartMonitoring(native);
        }


        protected override void StartRangingNative(BeaconRegion region) {
            var native = this.ToNative(region);
            this.beaconManager.StartRangingBeacons(native);
        }


		protected override void StopMonitoringNative(BeaconRegion region) {
			var native = this.ToNative(region);
            this.beaconManager.StopMonitoring(native);
        }


        protected override void StopRangingNative(BeaconRegion region) {
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


		protected virtual Beacon FromNative(Estimote.Beacon native) {
			var prox = this.FromNative(native.Proximity);
			var beacon = new Beacon(
				native.ProximityUUID.AsString(),
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