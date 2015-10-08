using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Android.App;
using EstimoteSdk;
using Android.Content;
using Android.Util;
using Timer = System.Timers.Timer;


namespace Estimotes {

    public class BeaconManagerImpl : AbstractBeaconManagerImpl {
        const string DEBUG_TAG = "acrbeacons";
		readonly IDictionary<string, IEddystoneFilter> filters;
		readonly IList<Beacon> beaconsInRange;
		readonly BeaconManager beaconManager;
		readonly Timer rangeTimer;
        bool isConnected;


        public BeaconManagerImpl() {
            this.beaconManager = new BeaconManager(Application.Context);
			this.rangeTimer = new Timer(500); // every second TODO: should coincide with foreground timer
			this.beaconsInRange = new List<Beacon>();
			this.filters = new Dictionary<string,IEddystoneFilter>();

			this.rangeTimer.Elapsed += (sender, args) => {
				this.rangeTimer.Stop();
				lock (this.beaconsInRange) {
					var count = this.beaconsInRange.Count;
					for (var i = 0; i < count; i++) {
						var b = this.beaconsInRange[i];
						var ts = b.LastPing.Subtract(DateTime.UtcNow);
						if (ts.TotalMilliseconds <= -2000) {
							this.beaconsInRange.RemoveAt(i);
							i--;
							count--;
						}
					}
				}
				this.rangeTimer.Start();
			};
            this.beaconManager.EnteredRegion += (sender, args) => {
                Log.Debug(DEBUG_TAG, "EnteredRegion Event");
                var region = this.FromNative(args.Region);
                this.OnRegionStatusChanged(region, true);
            };
            this.beaconManager.ExitedRegion += (sender, args) => {
                Log.Debug(DEBUG_TAG, "ExitedRegion Event");
                var region = this.FromNative(args.Region);
                this.OnRegionStatusChanged(region, false);
            };
            this.beaconManager.Ranging += (sender, args) => {
                Log.Debug(DEBUG_TAG, "Ranging Event");
				var beacons = args.Beacons.Select(x => new Beacon(x)).ToList();
                if (beacons.Count == 0)
                    return;

                List<Beacon> copy;
				lock (this.beaconsInRange) {
					foreach (var beacon in beacons) {
						var index = this.GetIndexOfBeacon(beacon);

						if (beacon.Proximity == Proximity.Unknown) {
							if (index > -1)
								this.beaconsInRange.RemoveAt(index);
						}
						else {
							beacon.LastPing = DateTime.UtcNow;
							if (index == -1)
								this.beaconsInRange.Add(beacon);

							else {
								var b = this.beaconsInRange[index];
								b.Proximity = beacon.Proximity;
								b.LastPing = beacon.LastPing;
							}
						}
					}
                    copy = this.beaconsInRange.ToList();
				}
				this.OnRanged(copy);
            };
            this.beaconManager.Eddystone += (sender, args) => {
				var eddystones = args
					.Eddystones
					.Select(x => new Eddystone(x))
					.Where(this.IsFiltered)
					.ToList();
				this.OnEddystone(eddystones);
            };
//            this.beaconManager.Nearable += (sender, args) => {
//                var list = args.Nearables.Select(x => new Nearable(x));
//                this.OnNearables(list);
//            };
        }


        public void SetBackgroundScanPeriod(TimeSpan scanPeriod, TimeSpan waitTime) {
            this.beaconManager.SetBackgroundScanPeriod((long)scanPeriod.TotalMilliseconds, (long)waitTime.TotalMilliseconds);
        }


        public void SetForegroundScanPeriod(TimeSpan scanPeriod, TimeSpan waitTime) {
            this.beaconManager.SetForegroundScanPeriod((long)scanPeriod.TotalMilliseconds, (long)waitTime.TotalMilliseconds);
        }


        public override async Task<BeaconInitStatus> Initialize(bool backgroundMonitoring) {
            if (this.isConnected)
                return BeaconInitStatus.Success;

            if (!this.beaconManager.HasBluetooth)
                return BeaconInitStatus.BluetoothMissing;

            if (!this.beaconManager.IsBluetoothEnabled)
				return BeaconInitStatus.BluetoothOff;

			//if (!this.beaconManager.CheckPermissionsAndService())
				//return BeaconInitStatus.PermissionDenied;

            await this.Connect();

            // restore monitored beacons
            foreach (var region in this.MonitoringRegions)
                this.StartMonitoringNative(region);

            return BeaconInitStatus.Success;
        }


        string esScanId;
        public override void StartEddystoneScanNative(IEddystoneFilter filter) {
            if (this.esScanId == null)
                this.esScanId = this.beaconManager.StartEddystoneScanning();

			this.filters.Add(filter.ToString(), filter);
        }


        public override void StopEddystoneScanNative(IEddystoneFilter filter) {
            if (this.esScanId != null)
                this.beaconManager.StopEddystoneScanning(this.esScanId);

			this.filters.Remove(filter.ToString());
        }


//        string nearableScanId;
//        public override void StartNearableDiscovery() {
//            if (this.nearableScanId == null)
//                this.nearableScanId = this.beaconManager.StartNearableDiscovery();
//        }
//
//
//        public override void StopNearableDiscovery() {
//            if (this.nearableScanId != null)
//                this.beaconManager.StopNearableDiscovery(this.nearableScanId);
//        }


        readonly ManualResetEvent locker = new ManualResetEvent(true);
        protected virtual async Task Connect() {
            await Task.Factory.StartNew(() => {
                this.locker.WaitOne();

                if (this.isConnected)
                    Log.Debug(DEBUG_TAG, "already connected to estimote service");
                else {
                    Application.Context.StartService(new Intent(Application.Context, typeof(EstimoteSdk.Service.BeaconService)));
			        var ready = new ServiceReadyCallbackImpl(() => {
                        Log.Debug(DEBUG_TAG, "successfully connected to estimote service");
				        this.isConnected = true;
                        this.locker.Set();
			        });
    		        this.beaconManager.Connect(ready);
                }
            });
            await Task.Delay(300); // let android get its stuff together
        }


        protected override void StartMonitoringNative(BeaconRegion region) {
            Log.Debug(DEBUG_TAG, "StartMonitoringNative");
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
			lock (this.beaconsInRange)
				this.beaconsInRange.Clear(); // TODO: could clear this smart.
        }


        protected virtual BeaconRegion FromNative(Region native) {
            ushort major = 0;
            ushort minor = 0;
            try {
                major = (ushort)native.Major;
                minor = (ushort)native.Minor;
            }
            catch {}
            return new BeaconRegion(
                native.Identifier,
                native.ProximityUUID,
			    major,
			    minor
            );
        }

        protected virtual Region ToNative(BeaconRegion region) {
			Region native = null;

			if (region.Major > 0 && region.Minor > 0)
				native = new Region(region.Identifier, region.Uuid, region.Major.Value, region.Minor.Value);

			else if (region.Major > 0)
				native = new Region(region.Identifier, region.Uuid, region.Major.Value);

			else
				native = new Region(region.Identifier, region.Uuid);

            return native;
        }


		protected override void UpdateRangingList() {
			base.UpdateRangingList();
			if (this.rangeTimer == null)
				return;

			if (this.RangingRegions.Count == 0) {
				if (this.rangeTimer.Enabled)
					this.rangeTimer.Stop();
			}
			else {
				if (!this.rangeTimer.Enabled)
					this.rangeTimer.Start();
			}
		}



		int GetIndexOfBeacon(Beacon beacon) {
			for (var i = 0; i < this.beaconsInRange.Count; i++) {
				var b = this.beaconsInRange[i];
				if (b.Uuid.Equals(beacon.Uuid, StringComparison.InvariantCultureIgnoreCase) && b.Major == beacon.Major && b.Minor == beacon.Minor)
					return i;
			}
			return -1;
		}


		bool IsFiltered(IEddystone eddystone) {
			if (eddystone.Type == EddystoneType.Url) {
				if (this.filters.ContainsKey(eddystone.Url))
					return true;

				var c = this.filters.Any(x => eddystone.Url.StartsWith(x.Key, StringComparison.InvariantCultureIgnoreCase));
				if (c)
					return true;
			}
			else {
				var key = eddystone.Namespace + eddystone.Instance;
				if (this.filters.ContainsKey(key))
					return true;

				if (this.filters.ContainsKey(eddystone.Namespace))
					return true;
			}
			return false;
		}
    }
}