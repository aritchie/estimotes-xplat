using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Reactive.Linq;
using Acr.Settings;


namespace Estimotes {

    public abstract class AbstractBeaconManagerImpl : IBeaconManager {
		const string SETTING_KEY = "beacons-monitor";
		readonly IList<BeaconRegion> monitoringRegions;
		readonly IList<BeaconRegion> rangingRegions;
		readonly IList<IEddystoneFilter> eddystoneFilters;


		protected AbstractBeaconManagerImpl() {
			Settings.Local.KeysNotToClear.Add(SETTING_KEY);
			this.monitoringRegions = Settings.Local.Get(SETTING_KEY, new List<BeaconRegion>());
			this.rangingRegions = new List<BeaconRegion>();
			this.eddystoneFilters = new List<IEddystoneFilter>();
			this.EddystoneFilters = new ReadOnlyCollection<IEddystoneFilter>(this.eddystoneFilters);

			this.UpdateMonitoringList();
			this.UpdateRangingList();

            this.WhenRegionStatusChanges = Observable
                .FromEventPattern<BeaconRegionStatusChangedEventArgs>(
                    x => this.RegionStatusChanged += x,
                    x => this.RegionStatusChanged -= x
                )
                .Select(x => x.EventArgs);

            this.WhenRanged = Observable
                .FromEventPattern<IEnumerable<IBeacon>>(
                    x => this.Ranged += x,
                    x => this.Ranged -= x
                )
                .Select(x => x.EventArgs);

            this.WhenEddystone = Observable
				.FromEventPattern<IEnumerable<IEddystone>>(
                    x => this.Eddystone += x,
                    x => this.Eddystone -= x
                )
				.Select(x => x.EventArgs);

//            this.WhenNearables = Observable
//                .FromEventPattern<IEnumerable<INearable>>(
//                    x => this.Nearables += x,
//                    x => this.Nearables -= x
//                )
//                .Select(x => x.EventArgs);
		}


        public abstract Task<BeaconInitStatus> Initialize(bool backgroundMonitoring);
		public abstract void StartEddystoneScanNative(IEddystoneFilter filter);
		public abstract void StopEddystoneScanNative(IEddystoneFilter filter);
//        public abstract void StartNearableDiscovery();
//        public abstract void StopNearableDiscovery();
        protected abstract void StartMonitoringNative(BeaconRegion region);
        protected abstract void StartRangingNative(BeaconRegion region);
        protected abstract void StopMonitoringNative(BeaconRegion region);
        protected abstract void StopRangingNative(BeaconRegion region);


		public virtual void StartMonitoring(BeaconRegion region) {
            this.StartMonitoringNative(region);
			this.monitoringRegions.Add(region);
			this.UpdateMonitoringList();
		}


		public virtual void StopMonitoring(BeaconRegion region) {
			this.monitoringRegions.Remove(region);
            this.StopMonitoringNative(region);
            this.UpdateMonitoringList();
		}


		public virtual void StartRanging(BeaconRegion region) {
            this.StartRangingNative(region);
			this.rangingRegions.Add(region);
            this.UpdateRangingList();
		}


		public virtual void StopRanging(BeaconRegion region) {
			this.rangingRegions.Remove(region);
            this.StopRangingNative(region);
            this.UpdateRangingList();
		}


		public virtual void StartEddystoneScan(IEddystoneFilter filter) {
			this.StartEddystoneScanNative(filter);
			this.eddystoneFilters.Add(filter); // TODO: implement equals on filter
			this.EddystoneFilters = new ReadOnlyCollection<IEddystoneFilter>(this.eddystoneFilters);
		}


		public virtual void StopEddystoneScan(IEddystoneFilter filter) {
			this.StopEddystoneScanNative(filter);
			this.eddystoneFilters.Remove(filter); // TODO: implement equals on filter
			this.EddystoneFilters = new ReadOnlyCollection<IEddystoneFilter>(this.eddystoneFilters);
		}


		public virtual void StopAllMonitoring() {
			var list = this.monitoringRegions.ToList();
			foreach (var region in list)
				this.StopMonitoringNative(region);

            this.monitoringRegions.Clear();
            this.UpdateMonitoringList();
		}


		public virtual void StopAllRanging() {
            var list = this.rangingRegions.ToList();
			foreach (var region in list)
				this.StopRangingNative(region);

            this.rangingRegions.Clear();
            this.UpdateRangingList();
		}


        public virtual async Task<IEnumerable<IBeacon>> FetchNearbyBeacons(BeaconRegion region, TimeSpan? waitTime) {
            var beaconList = new Dictionary<string, IBeacon>();
            var handler = new EventHandler<IEnumerable<IBeacon>>((sender, beacons) => {
                var list = beacons.Where(x => x.Uuid.Equals(region.Uuid));

                if (region.Major > 0)
                    list = list.Where(x => x.Major == region.Major.Value);

                if (region.Minor > 0)
                    list = list.Where(x => x.Minor == region.Minor.Value);

                foreach (var beacon in list) {
                    var key = $"{beacon.Uuid}-{beacon.Major}-{beacon.Minor}";
                    beaconList[key] = beacon;
                }
            });
            var wasRanging = true;
            if (!this.RangingRegions.Contains(region)) {
                this.StartRanging(region);
                wasRanging = false;
            }
            this.Ranged += handler;
            await Task.Delay(waitTime ?? TimeSpan.FromSeconds(3));
            this.Ranged -= handler;

            if (!wasRanging)
                this.StopRanging(region);

            return beaconList.Values;
        }


		public IReadOnlyList<BeaconRegion> RangingRegions { get; private set; }
		public IReadOnlyList<BeaconRegion> MonitoringRegions { get; private set; }
		public IReadOnlyList<IEddystoneFilter> EddystoneFilters { get; private set; }

        public IObservable<BeaconRegionStatusChangedEventArgs> WhenRegionStatusChanges { get; }
        public IObservable<IEnumerable<IBeacon>> WhenRanged { get; }
		public IObservable<IEnumerable<IEddystone>> WhenEddystone { get; }
//        public IObservable<IEnumerable<INearable>> WhenNearables { get; }


        public event EventHandler<IEnumerable<IBeacon>> Ranged;
        public event EventHandler<BeaconRegionStatusChangedEventArgs> RegionStatusChanged;
		public event EventHandler<IEnumerable<IEddystone>> Eddystone;
//        public event EventHandler<IEnumerable<INearable>> Nearables;


        protected virtual void OnRanged(IEnumerable<IBeacon> beacons) {
            this.Ranged?.Invoke(this, beacons);
        }


        protected virtual void OnRegionStatusChanged(BeaconRegion region, bool entering) {
            this.RegionStatusChanged?.Invoke(this, new BeaconRegionStatusChangedEventArgs(region, entering));
        }


		protected virtual void OnEddystone(IEnumerable<IEddystone> eddystones) {
			this.Eddystone?.Invoke(this, eddystones);
        }


//        protected virtual void OnNearables(IEnumerable<INearable> nearables) {
//            this.Nearables?.Invoke(this, nearables);
//        }


        protected virtual void UpdateMonitoringList() {
			if (this.monitoringRegions.Any())
				Settings.Local.Set(SETTING_KEY, this.monitoringRegions);
			else
				Settings.Local.Remove(SETTING_KEY);

			this.MonitoringRegions = new ReadOnlyCollection<BeaconRegion>(this.monitoringRegions);
		}


		protected virtual void UpdateRangingList() {
			this.RangingRegions = new ReadOnlyCollection<BeaconRegion>(this.rangingRegions);
		}
    }
}
