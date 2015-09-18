using System;
using CoreLocation;


namespace Estimotes {

    public class Beacon : IBeacon {
		readonly CLBeacon beacon;


        public Beacon(CLBeacon beacon) {
            this.beacon = beacon;
            this.Uuid = this.beacon.ProximityUuid.AsString();
        }


        public string Uuid { get; }
        public ushort Major => this.beacon.Major?.UInt16Value ?? 0;
        public ushort Minor => this.beacon.Minor?.UInt16Value ?? 0;
        public Proximity Proximity => this.beacon.Proximity.FromNative();
    }
}