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
		}


        public abstract Task<BeaconInitStatus> Initialize();
        protected abstract void StartMonitoringNative(BeaconRegion region);
        protected abstract void StartRangingNative(BeaconRegion region);
        protected abstract void StopMonitoringNative(BeaconRegion region);
        protected abstract void StopRangingNative(BeaconRegion region);


		public virtual bool StartMonitoring(BeaconRegion region) {
			if (this.monitoringRegions.Any(x => x.Uuid.Equals(region.Uuid)))
                return false;

            this.StartMonitoringNative(region);
			this.monitoringRegions.Add(region);
			this.UpdateMonitoringList();
            return true;
		}


		public virtual bool StopMonitoring(BeaconRegion region) {
			if (!this.monitoringRegions.Remove(region))
                return false;

            this.StopMonitoringNative(region);
            this.UpdateMonitoringList();
            return true;
		}


		public virtual bool StartRanging(BeaconRegion region) {
			if (this.rangingRegions.Any(x => x.Uuid.Equals(region.Uuid)))
                return false;

            this.StartRangingNative(region);
			this.rangingRegions.Add(region);
            this.UpdateRangingList();
            return true;
		}


		public virtual bool StopRanging(BeaconRegion region) {
			if (!this.rangingRegions.Remove(region))
                return false;

            this.StopRangingNative(region);
            this.UpdateRangingList();
            return true;
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

        public event EventHandler<IEnumerable<Beacon>> Ranged;
        public event EventHandler<BeaconRegionStatusChangedEventArgs> RegionStatusChanged;


        protected virtual void OnRanged(IEnumerable<Beacon> beacons) {
            this.Ranged?.Invoke(this, beacons);
        }


        protected virtual void OnRegionStatusChanged(BeaconRegion region, bool entering) {
            this.RegionStatusChanged?.Invoke(this, new BeaconRegionStatusChangedEventArgs(region, entering));
        }


		protected virtual void UpdateMonitoringList() {
			Settings.Local.Set(SETTING_KEY, this.monitoringRegions);
			this.MonitoringRegions = new ReadOnlyCollection<BeaconRegion>(this.monitoringRegions);
		}


		protected virtual void UpdateRangingList() {
			this.RangingRegions = new ReadOnlyCollection<BeaconRegion>(this.rangingRegions);
		}
    }
}
