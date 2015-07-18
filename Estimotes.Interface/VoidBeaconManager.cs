using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Estimotes {

    public class VoidBeaconManager : IBeaconManager {

        public async Task<bool> Initialize() { return false; }
		public void StartMonitoring(BeaconRegion region) {}
        public void StartRanging(BeaconRegion region) {}
		public void StopMonitoring(BeaconRegion region) {}
		public void StopRanging(BeaconRegion region) {}

        public event EventHandler<IEnumerable<Beacon>> Ranged;
        public event EventHandler<BeaconRegion> EnteredRegion;
        public event EventHandler<BeaconRegion> ExitedRegion;
    }
}