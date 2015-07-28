using System;
using CoreLocation;


namespace Estimotes {

    public class Beacon : IBeacon {
		readonly Estimote.Beacon beacon;


        public Beacon(Estimote.Beacon beacon) {
            this.beacon = beacon;

            this.Uuid = this.beacon.ProximityUUID.AsString();
            switch (beacon.Proximity) {

                case CLProximity.Far:
                    this.Proximity = Proximity.Far;
                    break;

                case CLProximity.Immediate:
                    this.Proximity = Proximity.Immediate;
                    break;

                case CLProximity.Near:
                    this.Proximity = Proximity.Near;
                    break;

                default:
                    this.Proximity = Proximity.Unknown;
                    break;
            }
        }


        public string Uuid { get; }
        public ushort Major => this.beacon.Major;
        public ushort Minor => this.beacon.Minor;
        public Proximity Proximity { get; }
    }
}