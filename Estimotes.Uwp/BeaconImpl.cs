using System;
using UniversalBeaconLibrary.Beacon;


namespace Estimotes {

    public class BeaconImpl : AbstractBeacon {
        readonly Beacon beacon;

        public BeaconImpl(Beacon beacon) : base(null, Proximity.Unknown, 0, 0) {
            this.beacon = beacon;
        }
    }
}
