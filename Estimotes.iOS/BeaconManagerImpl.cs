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
        readonly NearableManager nearableManager;


        public BeaconManagerImpl() {
            this.beaconManager = new BeaconManager {
                ReturnAllRangedBeaconsAtOnce = true
            };
            this.nearableManager = new NearableManager();
			// TODO: make this determining state configurable.
//			this.beaconManager.DeterminedState += (sender, args) => {
//				if (args.State == CLRegionState.Unknown)
//					return;
//
//				var region = this.FromNative(args.Region);
//				var entering = (args.State == CLRegionState.Inside);
//				this.OnRegionStatusChanged(region, entering);
//			};
            //this.nearableManager.RangedNearables
            this.beaconManager.EnteredRegion += (sender, args) => {
                var region = this.FromNative(args.Region);
                this.OnRegionStatusChanged(region, true);
            };
            this.beaconManager.ExitedRegion += (sender, args) => {
				var region = this.FromNative(args.Region);
                this.OnRegionStatusChanged(region, false);
            };
			this.beaconManager.RangedBeacons += (sender, args) => {
				var beacons = args.Beacons.Select(x => new Beacon(args.Region, x));
				this.OnRanged(beacons);
            };
        }



        public override async Task<BeaconInitStatus> Initialize() {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(7, 0))
                return BeaconInitStatus.InvalidOperatingSystem;

            if (!CLLocationManager.LocationServicesEnabled)
                return BeaconInitStatus.LocationServicesDisabled;

//			if (UIApplication.SharedApplication.BackgroundRefreshStatus != UIBackgroundRefreshStatus.Denied)
//				return;
            //if (!CLLocationManager.IsMonitoringAvailable())
            //CLLocationManager.IsRangingAvailable
//            var btstatus = this.GetBluetoothStatus();
//            if (btstatus != BeaconInitStatus.Success)
//                return btstatus;

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


//        protected virtual BeaconInitStatus GetBluetoothStatus() {
//            using (var cb = new CBCentralManager()) {
//				cb.
//                switch (cb.State) {
//                    case CBCentralManagerState.Unauthorized: return BeaconInitStatus.PermissionDenied;
//                    case CBCentralManagerState.Unsupported: return BeaconInitStatus.BluetoothMissing;
//                    case CBCentralManagerState.PoweredOn: return BeaconInitStatus.Success;
//                    case CBCentralManagerState.PoweredOff: return BeaconInitStatus.BluetoothOff;
//                    default : return BeaconInitStatus.Unknown;
//                }
//            }
//        }


		protected virtual bool IsGoodStatus(CLAuthorizationStatus status) {
			return (
			    status == CLAuthorizationStatus.Authorized ||
			    status == CLAuthorizationStatus.AuthorizedAlways ||
			    status == CLAuthorizationStatus.AuthorizedWhenInUse
			);
		}


        public override string StartNearableDiscovery() {
            //this.nearableManager.StartRanging(NearableType.All);
            return null;
        }


        public override void StopNearableDiscovery(string id) {
            //this.nearableManager.StopRanging();
        }


        protected override void StartMonitoringNative(BeaconRegion region) {
			var native = this.ToNative(region);
            this.beaconManager.StartMonitoring(native);
			this.beaconManager.RequestState(native);
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
			Estimote.BeaconRegion native = null;

			if (region.Major > 0 && region.Minor > 0)
				native = new Estimote.BeaconRegion(uuid, region.Major.Value, region.Minor.Value, region.Identifier);

			else if (region.Major > 0)
				native = new Estimote.BeaconRegion(uuid, region.Major.Value, region.Identifier);

			else
				native = new Estimote.BeaconRegion(uuid, region.Identifier);

			native.NotifyEntryStateOnDisplay = true;
			native.NotifyOnEntry = true;
			native.NotifyOnExit = true;

			return native;
        }
    }
}