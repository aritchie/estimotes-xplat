using System;
using Native = EstimoteSdk.EddystoneSdk.Eddystone;

namespace Estimotes {

    public class Eddystone : IEddystone {
        readonly Native native;


        public Eddystone(Native native) {
            this.native = native;
        }


        public int Rssi => this.native.Rssi;
        public double Temperature => this.native.Telemetry.Temperature;
        public EddystoneType Type => this.native.IsUrl ? EddystoneType.Url : EddystoneType.Uid;
        public string Url => this.native.Url;
    }
}