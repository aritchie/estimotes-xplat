using System;
using System.Collections.Generic;
using System.Text;
using Estimote;
using Foundation;
using N = Estimote.Eddystone;

namespace Estimotes {

    public class AcrEddystoneDelegate : EddystoneManagerDelegate {
        readonly Action<N[]> onDiscovered;


        public AcrEddystoneDelegate(Action<N[]> onDiscovered) {
            this.onDiscovered = onDiscovered;
        }

        public override void DiscoveredEddystones(EddystoneManager manager, N[] eddystones, EddystoneFilter eddystoneFilter) {
            this.onDiscovered(eddystones);
            base.DiscoveredEddystones(manager, eddystones, eddystoneFilter);
        }


        public override void DiscoveryFailed(EddystoneManager manager, NSError error) {
            Console.WriteLine(error.ToString());
            base.DiscoveryFailed(manager, error);
        }
    }
}
