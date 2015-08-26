using System;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Estimotes {

    public interface IBeaconManager {

        /// <summary>
        /// This must be called in order to initialize any sort of monitoring or scanning
        /// </summary>
        /// <returns></returns>
        Task<BeaconInitStatus> Initialize(bool backgroundMonitoring = true);

        //Task StartAdvertising();
        //Task StopAdvertising();

        /// <summary>
        /// Starts background monitoring for beacon region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
		void StartMonitoring(BeaconRegion region);

        /// <summary>
        /// Stops background monitoring for beacon region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
		void StopMonitoring(BeaconRegion region);

        event EventHandler<BeaconRegionStatusChangedEventArgs> RegionStatusChanged;

        /// <summary>
        /// Stops background monitoring for beacon region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        void StartRanging(BeaconRegion region);

        /// <summary>
        /// Stops background monitoring for beacon region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        void StopRanging(BeaconRegion region);
        event EventHandler<IEnumerable<IBeacon>> Ranged;

        //void StartNearableDiscovery();
        //void StopNearableDiscovery();

		IReadOnlyList<BeaconRegion> MonitoringRegions { get; }
		IReadOnlyList<BeaconRegion> RangingRegions { get; }
//        event EventHandler<IEnumerable<INearable>> Nearables;

		void StopAllMonitoring();
		void StopAllRanging();

        Task<IEnumerable<IBeacon>> FetchNearbyBeacons(BeaconRegion region, TimeSpan? waitTime = null);

        IObservable<BeaconRegionStatusChangedEventArgs> WhenRegionStatusChanges { get; }
		IObservable<IEnumerable<IBeacon>> WhenRanged { get; }
    }
}
