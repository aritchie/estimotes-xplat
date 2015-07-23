using System;

namespace Estimotes {

    public enum BeaconInitStatus {
        Unknown,
        PermissionDenied,
        InvalidOperatingSystem,
        LocationServicesDisabled,
        BluetoothMissing,
        BluetoothOff,
        Success
    }
}
