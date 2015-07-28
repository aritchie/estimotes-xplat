using System;


namespace Estimotes {

    public interface IBeacon {

		Proximity Proximity { get; }
		ushort Minor { get; }
		ushort Major { get; }
		string Uuid { get; }
    }
}

		// TODO: colour, distance, power?
        // TODO: readtemperatureasync
        // TODO: getbatterylevel and battery life remaining
