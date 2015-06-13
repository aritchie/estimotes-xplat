using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Estimotes {

    public abstract class AbstractBeaconManagerImpl : IBeaconManager {

        public abstract Task<bool> Initialize();
        public abstract void StartMonitoring(params BeaconRegion[] regions);
        public abstract void StopMonitoring(params BeaconRegion[] regions);
        public abstract void StartRanging(params BeaconRegion[] regions);
        public abstract void StopRanging(params BeaconRegion[] regions);


        public event EventHandler<IEnumerable<Beacon>> Ranged;
        public event EventHandler<BeaconRegion> EnteredRegion;
        public event EventHandler<BeaconRegion> ExitedRegion;


        protected virtual void OnRanged(IEnumerable<Beacon> beacons) {
            if (this.Ranged != null)
                this.Ranged(this, beacons);
        }


        protected virtual void OnEnteredRegion(BeaconRegion region) {
            if (this.EnteredRegion != null)
                this.EnteredRegion(this, region);
        }


        protected virtual void OnExitedRegion(BeaconRegion region) {
            if (this.ExitedRegion != null)
                this.ExitedRegion(this, region);
        }
    }
}
