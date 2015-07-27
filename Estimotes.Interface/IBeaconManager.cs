using System;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Estimotes {

    public interface IBeaconManager {

        /// <summary>
        /// This must be called in order to initialize any sort of monitoring or scanning
        /// </summary>
        /// <returns></returns>
        Task<BeaconInitStatus> Initialize();

        /// <summary>
        /// Starts background monitoring for beacon region.  Returns false if beacon identifier is already being monitored
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
		bool StartMonitoring(BeaconRegion region);

        /// <summary>
        /// Stops background monitoring for beacon region.  Returns false if beacon identifier was not being monitored
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
		bool StopMonitoring(BeaconRegion region);

        event EventHandler<BeaconRegionStatusChangedEventArgs> RegionStatusChanged;

        /// <summary>
        /// Stops background monitoring for beacon region.  Returns false if beacon identifier was not being monitored
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        bool StartRanging(BeaconRegion region);

        /// <summary>
        /// Stops background monitoring for beacon region.  Returns false if beacon identifier was not being monitored
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        bool StopRanging(BeaconRegion region);
        event EventHandler<IEnumerable<IBeacon>> Ranged;

//        string StartNearableDiscovery();
//        void StopNearableDiscovery(string id);

		IReadOnlyList<BeaconRegion> MonitoringRegions { get; }
		IReadOnlyList<BeaconRegion> RangingRegions { get; }
//        event EventHandler<IEnumerable<INearable>> Nearables;

		void StopAllMonitoring();
		void StopAllRanging();
    }
}
