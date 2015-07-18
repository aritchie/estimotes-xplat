using System;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Estimotes {

    public interface IBeaconManager {

        Task<bool> Initialize();

		void StartMonitoring(BeaconRegion region);
		void StopMonitoring(BeaconRegion region);
        event EventHandler<BeaconRegion> EnteredRegion;
        event EventHandler<BeaconRegion> ExitedRegion;

        void StartRanging(BeaconRegion region);
        void StopRanging(BeaconRegion region);
        event EventHandler<IEnumerable<Beacon>> Ranged;

		IReadOnlyList<BeaconRegion> MonitoringRegions { get; }
		IReadOnlyList<BeaconRegion> RangingRegions { get; }
		void StopAllMonitoring();
		void StopAllRanging();
    }
}
