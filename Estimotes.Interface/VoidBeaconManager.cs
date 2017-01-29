using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;


namespace Estimotes
{

    public class VoidBeaconManager : IBeaconManager
    {

        public Task<BeaconInitStatus> Initialize(bool backgroundMonitoring) => Task.FromResult(BeaconInitStatus.Unknown);
        public void StartMonitoring(BeaconRegion region) { }
        public void StartRanging(BeaconRegion region) { }
        public void StopMonitoring(BeaconRegion region) { }
        public void StopRanging(BeaconRegion region) { }
        public void StopAllMonitoring() { }
        public void StopAllRanging() { }


        public async Task<IEnumerable<IBeacon>> FetchNearbyBeacons(BeaconRegion region, TimeSpan? time)
        {
            return await Task.FromResult(new List<IBeacon>(0));
        }

        public event EventHandler<IEnumerable<IBeacon>> Ranged;
        public event EventHandler<BeaconRegionStatusChangedEventArgs> RegionStatusChanged;

        public IReadOnlyList<BeaconRegion> MonitoringRegions { get; } = new ReadOnlyCollection<BeaconRegion>(new List<BeaconRegion>(0));
        public IReadOnlyList<BeaconRegion> RangingRegions { get; } = new ReadOnlyCollection<BeaconRegion>(new List<BeaconRegion>(0));


        public IObservable<BeaconRegionStatusChangedEventArgs> WhenRegionStatusChanges { get; } = Observable.Empty<BeaconRegionStatusChangedEventArgs>();
        public IObservable<IEnumerable<IBeacon>> WhenRanged { get; } = Observable.Empty<IEnumerable<IBeacon>>();
    }
}