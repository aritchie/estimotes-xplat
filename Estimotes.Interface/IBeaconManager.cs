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

        #region Advertising

        //Task StartAdvertising();
        //Task StopAdvertising();

        #endregion

        #region Monitoring

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

        /// <summary>
        /// Stops and clears all current monitoring
        /// </summary>
		void StopAllMonitoring();

        /// <summary>
        /// Beacon Region Monitoring Event
        /// </summary>
        event EventHandler<BeaconRegionStatusChangedEventArgs> RegionStatusChanged;

        /// <summary>
        /// Regions currently being monitored.  Persists through app and device restarts
        /// </summary>
		IReadOnlyList<BeaconRegion> MonitoringRegions { get; }

        /// <summary>
        /// Observable for region monitoring events
        /// </summary>
        IObservable<BeaconRegionStatusChangedEventArgs> WhenRegionStatusChanges { get; }

        /// <summary>
        /// Fetch all nearby beacons for a specific region.  This is effective for ranging after a monitor entered event.
        /// </summary>
        /// <param name="region"></param>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        Task<IEnumerable<IBeacon>> FetchNearbyBeacons(BeaconRegion region, TimeSpan? waitTime = null);

        #endregion

        #region Ranging

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

        /// <summary>
        /// Stops and clears all current ranging
        /// </summary>
		void StopAllRanging();

        /// <summary>
        /// Regions currently being ranged
        /// </summary>
		IReadOnlyList<BeaconRegion> RangingRegions { get; }

        /// <summary>
        /// Beacon Region Ranging Event
        /// </summary>
        event EventHandler<IEnumerable<IBeacon>> Ranged;

        /// <summary>
        /// Observable for region ranged events
        /// </summary>
		IObservable<IEnumerable<IBeacon>> WhenRanged { get; }

        #endregion


        #region Eddystone

        void StartEddystoneScan(IEddystoneFilter filter);
		void StopEddystoneScan(IEddystoneFilter filter);
		IReadOnlyList<IEddystoneFilter> EddystoneFilters { get; }
		event EventHandler<IEnumerable<IEddystone>> Eddystone;
		IObservable<IEnumerable<IEddystone>> WhenEddystone { get; }

        #endregion

//        #region Nearables
//        void StartNearableDiscovery();
//        void StopNearableDiscovery();
//        event EventHandler<IEnumerable<INearable>> Nearables;
//        IObservable<IEnumerable<INearable>> WhenNearables { get; }
//        #endregion
    }
}
