using ObjCRuntime;

namespace EstimoteSDK {
public enum ESTColor
{
	Unknown,
	MintCocktail,
	IcyMarshmallow,
	BlueberryPie,
	SweetBeetroot,
	CandyFloss,
	LemonTart,
	VanillaJello,
	LiquoriceSwirl,
	White,
	Transparent
}

public enum ESTFirmwareUpdate
{
	None,
	Available,
	Unsupported
}

public enum ESTConnectionStatus
{
	Disconnected,
	Connecting,
	Connected,
	Updating
}

public enum ESTBroadcastingScheme : sbyte
{
	Unknown,
	Estimote,
	IBeacon,
	EddystoneURL,
	EddystoneUID
}

[Native]
public enum ESTNearableType : long
{
	Unknown = 0,
	Dog,
	Car,
	Fridge,
	Bag,
	Bike,
	Chair,
	Bed,
	Door,
	Shoe,
	Generic,
	All
}

[Native]
public enum ESTNearableOrientation : long
{
	Unknown = 0,
	Horizontal,
	HorizontalUpsideDown,
	Vertical,
	VerticalUpsideDown,
	LeftSide,
	RightSide
}

[Native]
public enum ESTNearableZone : long
{
	Unknown = 0,
	Immediate,
	Near,
	Far
}

[Native]
public enum ESTNearableFirmwareState : long
{
	Boot = 0,
	App
}

public enum ESTBeaconPower : sbyte
{
	ESTBeaconPowerLevel1 = -30,
	ESTBeaconPowerLevel2 = -20,
	ESTBeaconPowerLevel3 = -16,
	ESTBeaconPowerLevel4 = -12,
	ESTBeaconPowerLevel5 = -8,
	ESTBeaconPowerLevel6 = -4,
	ESTBeaconPowerLevel7 = 0,
	ESTBeaconPowerLevel8 = 4
}

public enum ESTBeaconBatteryType
{
	Unknown = 0,
	Cr2450,
	Cr2477
}

[Native]
public enum ESTBeaconFirmwareState : long
{
	Boot,
	App
}

[Native]
public enum ESTBeaconPowerSavingMode : long
{
	Unknown,
	Unsupported,
	On,
	Off
}

[Native]
public enum ESTBeaconEstimoteSecureUUID : long
{
	Unknown,
	Unsupported,
	On,
	Off
}

[Native]
public enum ESTBeaconMotionUUID : long
{
	Unknown,
	Unsupported,
	On,
	Off
}

[Native]
public enum ESTBeaconMotionState : long
{
	Unknown,
	Unsupported,
	Moving,
	NotMoving
}

[Native]
public enum ESTBeaconTemperatureState : long
{
	Unknown,
	Unsupported,
	Supported
}

[Native]
public enum ESTBeaconMotionDetection : long
{
	Unknown,
	Unsupported,
	On,
	Off
}

[Native]
public enum ESTBeaconConditionalBroadcasting : long
{
	Unknown,
	Unsupported,
	Off,
	MotionOnly,
	FlipToStop
}

[Native]
public enum ESTBeaconCharInfoType : long
{
	Read,
	Only
}

public enum ESTConnection : uint
{
	InternetConnectionError,
	IdentifierMissingError,
	NotAuthorizedError,
	NotConnectedToReadWrite
}

[Native]
public enum ESTUtilitManagerState : long
{
	Idle,
	Scanning
}

[Native]
public enum ESTNotification : long
{
	SaveNearableZoneDescription,
	SaveNearable,
	BeaconEnterRegion,
	BeaconExitRegion,
	NearableEnterRegion,
	NearableExitRegion,
	RangeNearables
}

[Native]
public enum ESBeaconUpdateInfoStatus : long
{
	Idle,
	ReadyToUpdate,
	Updating,
	UpdateSuccess,
	UpdateFailed
}

[Native]
public enum ESBulkUpdaterStatus : long
{
	Idle,
	Updating,
	Completed
}

[Native]
public enum ESTBulkUpdaterMode : long
{
	Foreground,
	Background
}

[Native]
public enum ESTEddystoneProximity : long
{
	Unknown,
	Immediate,
	Near,
	Far
}

[Native]
public enum ESTEddystoneManagerState : long
{
	Idle,
	Scanning
}
}