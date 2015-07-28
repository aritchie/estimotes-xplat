using System;
using EstimoteSdk;


namespace Estimotes {

    public class Beacon : IBeacon {
        readonly EstimoteSdk.Beacon beacon;


        public Beacon(EstimoteSdk.Beacon beacon) {
            this.beacon = beacon;
			var prox = Utils.ComputeProximity(beacon);
			if (prox == Utils.Proximity.Far)
				this.Proximity = Proximity.Far;
			else if (prox == Utils.Proximity.Immediate)
				this.Proximity = Proximity.Immediate;
			else if (prox == Utils.Proximity.Near)
				this.Proximity = Proximity.Near;
			else
				this.Proximity = Proximity.Unknown;	
        }


        public string Uuid => this.beacon.ProximityUUID;
        public ushort Major => (ushort)this.beacon.Major;
        public ushort Minor => (ushort)this.beacon.Minor;
		public Proximity Proximity { get; internal set; }
		internal DateTime LastPing { get; set; }
    }
}