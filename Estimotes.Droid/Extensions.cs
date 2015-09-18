using System;
using EstimoteSdk;


namespace Estimotes {

    public static class Extensions {

        public static Proximity GetProximity(this EstimoteSdk.EddystoneSdk.Eddystone eddystone) {
            var prox = Utils.ComputeProximity(eddystone);
            return prox.FromNative();
        }


        public static Proximity GetProximity(this EstimoteSdk.Beacon beacon) {
            var prox = Utils.ComputeProximity(beacon);
            return prox.FromNative();
        }


        public static Proximity FromNative(this Utils.Proximity prox) {
            if (prox == Utils.Proximity.Far)
				return Proximity.Far;

            if (prox == Utils.Proximity.Immediate)
			    return Proximity.Immediate;

            if (prox == Utils.Proximity.Near)
				return Proximity.Near;

            return Proximity.Unknown;
        }
    }
}