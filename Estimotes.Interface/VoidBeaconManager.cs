using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace Estimotes {

	public class VoidBeaconManager : IBeaconManager {

		public VoidBeaconManager() {
			this.MonitoringRegions = new ReadOnlyCollection<BeaconRegion>(new List<BeaconRegion>(0));
			this.RangingRegions = new ReadOnlyCollection<BeaconRegion>(new List<BeaconRegion>(0));
		}


        public async Task<bool> Initialize() { return false; }
		public void StartMonitoring(BeaconRegion region) {}
        public void StartRanging(BeaconRegion region) {}
		public void StopMonitoring(BeaconRegion region) {}
		public void StopRanging(BeaconRegion region) {}

        public event EventHandler<IEnumerable<Beacon>> Ranged;
        public event EventHandler<BeaconRegion> EnteredRegion;
        public event EventHandler<BeaconRegion> ExitedRegion;

		public void StopAllMonitoring() {}
		public void StopAllRanging() {}


		public IReadOnlyList<BeaconRegion> MonitoringRegions { get; }
		public IReadOnlyList<BeaconRegion> RangingRegions { get; }
    }
}