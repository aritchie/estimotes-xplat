using System;
using CoreLocation;
using Estimote;


namespace Estimotes {

    public static class Extensions {

        public static Proximity FromNative(this Estimote.EddystoneProximity prox) {
            switch (prox) {
                case EddystoneProximity.Far:
                    return Proximity.Far;

                case EddystoneProximity.Immediate:
                    return Proximity.Immediate;

                case EddystoneProximity.Near:
                    return Proximity.Near;

                default:
                case EddystoneProximity.Unknown:
                    return Proximity.Unknown;
            }
        }



        public static Proximity FromNative(this CLProximity proximity) {
            switch (proximity) {

                case CLProximity.Far:
                    return Proximity.Far;

                case CLProximity.Immediate:
                    return Proximity.Immediate;

                case CLProximity.Near:
                    return Proximity.Near;

                default:
                    return Proximity.Unknown;
            }
        }
    }
}
