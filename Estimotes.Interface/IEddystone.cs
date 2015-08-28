using System;


namespace Estimotes {

    public interface IEddystone {

        string Url { get; }
        int Rssi { get; }
        double Temperature { get; }
        EddystoneType Type { get; }
    }
}
