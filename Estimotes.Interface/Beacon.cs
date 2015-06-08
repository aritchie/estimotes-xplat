using System;


namespace Estimotes {

    public class Beacon {

        public Proximity Proximity { get; private set; }
        public ushort? Minor { get; private set; }
        public ushort? Major { get; private set; }
        public BeaconRegion Region { get; private set; }


        public Beacon(BeaconRegion region, Proximity proximity, ushort minor, ushort major) {
            this.Region = region;
            this.Proximity = proximity;
            this.Minor = minor;
            this.Major = major;
        }


        // TODO: readtemperatureasync
        // TODO: getbatterylevel and battery life remaining
    }
}