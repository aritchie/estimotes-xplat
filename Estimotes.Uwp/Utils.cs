using System;
using UniversalBeaconLibrary.Beacon;


namespace Estimotes {

    public static class Utils {

        public static Proximity CalculateProximity(this Beacon beacon) {
            //beacon.Rssi
  /*
     * RSSI = TxPower - 10 * n * lg(d)
     * n = 2 (in free space)
     *
     * d = 10 ^ ((TxPower - RSSI) / (10 * n))
     */

    //return Math.pow(10d, ((double) txPower - rssi) / (10 * 2));
            return Proximity.Unknown;
        }
    }
}
