using System;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Estimotes {

    public interface IBeaconManager {

        Task<bool> IsAvailable();

        void StartMonitoring(params BeaconRegion[] regions);
        void StartRanging(params BeaconRegion[] regions);

        void StopMonitoring(params BeaconRegion[] regions);
        void StopRanging(params BeaconRegion[] regions);

        event EventHandler<IEnumerable<Beacon>> Ranged;
        event EventHandler<BeaconRegion> EnteredRegion;
        event EventHandler<BeaconRegion> ExitedRegion;
    }
}
