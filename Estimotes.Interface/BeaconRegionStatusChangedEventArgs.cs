using System;


namespace Estimotes {

    public class BeaconRegionStatusChangedEventArgs : EventArgs {

        public bool IsEntering { get; }
        public BeaconRegion Region { get; }


        public BeaconRegionStatusChangedEventArgs(BeaconRegion region, bool entering) {
            this.Region = region;
            this.IsEntering = entering;
        }
    }
}
