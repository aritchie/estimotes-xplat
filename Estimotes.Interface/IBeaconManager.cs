using System;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Estimotes {

    public interface IBeaconManager {

        Task<bool> Initialize();

        void StartMonitoring(string uuid, ushort? major = null, ushort? minor = null);
        void StopMonitoring(string uuid, ushort? major = null, ushort? minor = null);
        event EventHandler<BeaconRegion> EnteredRegion;
        event EventHandler<BeaconRegion> ExitedRegion;


        void StartRanging(BeaconRegion region);
        void StopRanging(BeaconRegion region);
        event EventHandler<IEnumerable<Beacon>> Ranged;
    }
}
