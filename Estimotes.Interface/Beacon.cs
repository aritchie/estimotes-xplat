using System;


namespace Estimotes {

    public class Beacon {

        public Proximity Proximity { get; private set; }
        public ushort? Minor { get; private set; }
        public ushort? Major { get; private set; }
		public string Identifier { get; private set; }
		public string Uuid { get; private set; }


		public Beacon(string uuid, string identifier, Proximity proximity, ushort minor, ushort major) {
			this.Uuid = uuid;
			this.Identifier = identifier;
            this.Proximity = proximity;
            this.Minor = minor;
            this.Major = major;
        }


        // TODO: readtemperatureasync
        // TODO: getbatterylevel and battery life remaining
    }
}