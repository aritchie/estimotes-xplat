using System;
using EstimoteSdk;


namespace Estimotes {

    public class Beacon : IBeacon {
        readonly EstimoteSdk.Beacon beacon;


        public Beacon(EstimoteSdk.Beacon beacon) {
            this.beacon = beacon;
            this.Proximity = beacon.GetProximity();
        }


        public string Uuid => this.beacon.ProximityUUID;
        public ushort Major => (ushort)this.beacon.Major;
        public ushort Minor => (ushort)this.beacon.Minor;
		public Proximity Proximity { get; internal set; }
		internal DateTime LastPing { get; set; }
    }
}