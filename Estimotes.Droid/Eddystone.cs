using System;
using EstimoteSdk;
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
        public Proximity Proximity => this.native.GetProximity();
        public string Namespace => this.native.Namespace;
        public string Instance => this.native.Instance;
        public string MacAddress => this.native.MacAddress;
        public string Url => this.native.Url;
    }
}