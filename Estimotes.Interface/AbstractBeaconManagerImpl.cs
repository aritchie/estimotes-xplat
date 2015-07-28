using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Acr.Settings;


namespace Estimotes {

    public abstract class AbstractBeaconManagerImpl : IBeaconManager {
		private const string SETTING_KEY = "beacons-monitor";
		readonly IList<BeaconRegion> monitoringRegions;
		readonly IList<BeaconRegion> rangingRegions;


		protected AbstractBeaconManagerImpl() {
			Settings.Local.KeysNotToClear.Add(SETTING_KEY);
			this.monitoringRegions = Settings.Local.Get(SETTING_KEY, new List<BeaconRegion>());
			this.rangingRegions = new List<BeaconRegion>();

			this.UpdateMonitoringList();
			this.UpdateRangingList();
		}


        public abstract Task<BeaconInitStatus> Initialize();
        protected abstract void StartMonitoringNative(BeaconRegion region);
        protected abstract void StartRangingNative(BeaconRegion region);
        protected abstract void StopMonitoringNative(BeaconRegion region);
        protected abstract void StopRangingNative(BeaconRegion region);
//        public abstract string StartNearableDiscovery();
//        public abstract void StopNearableDiscovery(string id);


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


		public IReadOnlyList<BeaconRegion> RangingRegions { get; private set; }
		public IReadOnlyList<BeaconRegion> MonitoringRegions { get; private set; }

        public event EventHandler<IEnumerable<IBeacon>> Ranged;
        public event EventHandler<BeaconRegionStatusChangedEventArgs> RegionStatusChanged;
//        public event EventHandler<IEnumerable<INearable>> Nearables;


        protected virtual void OnRanged(IEnumerable<IBeacon> beacons) {
            this.Ranged?.Invoke(this, beacons);
        }


        protected virtual void OnRegionStatusChanged(BeaconRegion region, bool entering) {
            this.RegionStatusChanged?.Invoke(this, new BeaconRegionStatusChangedEventArgs(region, entering));
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
