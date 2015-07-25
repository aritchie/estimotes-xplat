using System;


namespace Estimotes {

    public abstract class AbstractBeacon : IBeacon {


		protected AbstractBeacon(string uuid, Proximity proximity, ushort major, ushort minor) {
			this.Uuid = uuid;
            this.Proximity = proximity;
            this.Major = major;
			this.Minor = minor;
        }

        public string Identifier { get; }
        public ushort Major { get; }
        public ushort Minor { get; }
        public Proximity Proximity { get; }
        public string Uuid { get; }
    }
}