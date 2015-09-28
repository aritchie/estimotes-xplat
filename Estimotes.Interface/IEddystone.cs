using System;


namespace Estimotes {

    public interface IEddystone {

        string MacAddress { get; }
        string Namespace { get; }
        string Instance { get; }
        string Url { get; }
        int Rssi { get; }
        double? Temperature { get; }
        Proximity Proximity { get; }
        EddystoneType Type { get; }
    }
}
