using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace Estimotes {

	public class VoidBeaconManager : IBeaconManager {

        public async Task<BeaconInitStatus> Initialize() => BeaconInitStatus.Unknown;
		public bool StartMonitoring(BeaconRegion region) => false;
        public bool StartRanging(BeaconRegion region) => false;
		public bool StopMonitoring(BeaconRegion region) => false;
		public bool StopRanging(BeaconRegion region) => false;

        public event EventHandler<IEnumerable<IBeacon>> Ranged;
        public event EventHandler<BeaconRegionStatusChangedEventArgs> RegionStatusChanged;

		public void StopAllMonitoring() {}
		public void StopAllRanging() {}


		public IReadOnlyList<BeaconRegion> MonitoringRegions { get; } = new ReadOnlyCollection<BeaconRegion>(new List<BeaconRegion>(0));
		public IReadOnlyList<BeaconRegion> RangingRegions { get; } = new ReadOnlyCollection<BeaconRegion>(new List<BeaconRegion>(0));
    }
}