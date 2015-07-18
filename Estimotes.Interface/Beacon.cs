using System;


namespace Estimotes {

    public class Beacon {

		public Proximity Proximity { get; }
		public ushort? Minor { get; }
		public ushort? Major { get; }
		public string Uuid { get; }


		public Beacon(string uuid, Proximity proximity, ushort major, ushort minor) {
			this.Uuid = uuid;
            this.Proximity = proximity;
            this.Major = major;
			this.Minor = minor;
        }


		// TODO: colour, distance, power?
        // TODO: readtemperatureasync
        // TODO: getbatterylevel and battery life remaining
    }
}