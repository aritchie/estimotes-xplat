using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Estimotes {

    public class VoidBeaconManager : IBeaconManager {

        public async Task<bool> Initialize() { return false; }
        public void StartMonitoring(params BeaconRegion[] regions) {}
        public void StartRanging(params BeaconRegion[] regions) {}
        public void StopMonitoring(params BeaconRegion[] regions) {}
        public void StopRanging(params BeaconRegion[] regions) {}
        public void StopAllMonitoring() {}
        public void StopAllRanging() {}

        public event EventHandler<IEnumerable<Beacon>> Ranged;
        public event EventHandler<BeaconRegion> EnteredRegion;
        public event EventHandler<BeaconRegion> ExitedRegion;
    }
}