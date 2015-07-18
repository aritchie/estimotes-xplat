using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.Settings;


namespace Estimotes {

    public abstract class AbstractBeaconManagerImpl : IBeaconManager {
		private readonly IList<BeaconRegion> monitoringRegions;
		private readonly IList<BeaconRegion> rangingRegions;


		protected AbstractBeaconManagerImpl() {
			Settings.Local.KeysNotToClear.Add("beacons-monitor");
			this.monitoringRegions = Settings.Local.Get<List<BeaconRegion>>("beacons-monitor", new List<BeaconRegion>());
			this.rangingRegions = new List<BeaconRegion>();
		}


        public abstract Task<bool> Initialize();


		public virtual void StartMonitoring(BeaconRegion region) {
			this.monitoringRegions.Add(region);
			Settings.Local.Set("beacons-monitor", this.monitoringRegions);
		}


		public virtual void StopMonitoring(BeaconRegion region) {
			this.monitoringRegions.Remove(region);
			Settings.Local.Set("beacons-monitor", this.monitoringRegions);
		}


		public virtual void StartRanging(BeaconRegion region) {
			this.rangingRegions.Add(region);
		}


		public virtual void StopRanging(BeaconRegion region) {
			this.rangingRegions.Remove(region);
		}


		public virtual void StopAllMonitoring() {
			foreach (var region in this.monitoringRegions)
				this.StopMonitoring(region);
		}


		public virtual void StopAllRanging() {
			foreach (var region in this.rangingRegions)
				this.StopMonitoring(region);
		}


		public IReadOnlyList<BeaconRegion> RangingRegions {
			get { return new ReadOnlyCollection<BeaconRegion>(this.rangingRegions); }
		}


		public IReadOnlyList<BeaconRegion> MonitoringRegions {
			get { return new ReadOnlyCollection<BeaconRegion>(this.monitoringRegions); }
		}


        public event EventHandler<IEnumerable<Beacon>> Ranged;
        public event EventHandler<BeaconRegion> EnteredRegion;
        public event EventHandler<BeaconRegion> ExitedRegion;


        protected virtual void OnRanged(IEnumerable<Beacon> beacons) {
            this.Ranged?.Invoke(this, beacons);
        }


        protected virtual void OnEnteredRegion(BeaconRegion region) {
			this.EnteredRegion?.Invoke(this, region);
        }


        protected virtual void OnExitedRegion(BeaconRegion region) {
			this.ExitedRegion?.Invoke(this, region);
        }
    }
}
