using System;
using CoreBluetooth;
using CoreLocation;
using EstimoteSDK;
using Foundation;
using ObjCRuntime;

namespace EstimoteSDK {
// typedef void (^ESTCompletionBlock)(NSError *);
delegate void ESTCompletionBlock (NSError arg0);

// typedef void (^ESTObjectCompletionBlock)(idNSError *);
delegate void ESTObjectCompletionBlock (NSObject arg0, NSError arg1);

// typedef void (^ESTDataCompletionBlock)(NSData *NSError *);
delegate void ESTDataCompletionBlock (NSData arg0, NSError arg1);

// typedef void (^ESTNumberCompletionBlock)(NSNumber *NSError *);
delegate void ESTNumberCompletionBlock (NSNumber arg0, NSError arg1);

// typedef void (^ESTUnsignedShortCompletionBlock)(unsigned shortNSError *);
delegate void ESTUnsignedShortCompletionBlock (ushort arg0, NSError arg1);

// typedef void (^ESTBoolCompletionBlock)(BOOLNSError *);
delegate void ESTBoolCompletionBlock (bool arg0, NSError arg1);

// typedef void (^ESTStringCompletionBlock)(NSString *NSError *);
delegate void ESTStringCompletionBlock (string arg0, NSError arg1);

// typedef void (^ESTProgressBlock)(NSIntegerNSString *NSError *);
delegate void ESTProgressBlock (nint arg0, string arg1, NSError arg2);

// typedef void (^ESTArrayCompletionBlock)(NSArray *NSError *);
delegate void ESTArrayCompletionBlock (NSObject[] arg0, NSError arg1);

// typedef void (^ESTDictionaryCompletionBlock)(NSDictionary *NSError *);
delegate void ESTDictionaryCompletionBlock (NSDictionary arg0, NSError arg1);

// typedef void (^ESTCsRegisterCompletonBlock)(NSError *);
delegate void ESTCsRegisterCompletonBlock (NSError arg0);

// @interface ESTDefinitions : NSObject
[BaseType (typeof(NSObject))]
interface ESTDefinitions
{
	// +(NSString *)nameForEstimoteColor:(ESTColor)color;
	[Static]
	[Export ("nameForEstimoteColor:")]
	string NameForEstimoteColor (ESTColor color);
}

// @interface ESTNearableDefinitions : ESTDefinitions
[BaseType (typeof(ESTDefinitions))]
interface ESTNearableDefinitions
{
	// +(NSString *)nameForType:(ESTNearableType)type;
	[Static]
	[Export ("nameForType:")]
	string NameForType (ESTNearableType type);
}

// @interface ESTNearable : NSObject <NSCopying, NSCoding>
[BaseType (typeof(NSObject))]
interface ESTNearable : INSCopying, INSCoding
{
	// @property (readonly, nonatomic, strong) NSString * identifier;
	[Export ("identifier", ArgumentSemantic.Strong)]
	string Identifier { get; }

	// @property (readonly, assign, nonatomic) ESTNearableZone zone;
	[Export ("zone", ArgumentSemantic.Assign)]
	ESTNearableZone Zone { get; }

	// @property (readonly, assign, nonatomic) ESTNearableType type;
	[Export ("type", ArgumentSemantic.Assign)]
	ESTNearableType Type { get; }

	// @property (readonly, assign, nonatomic) ESTColor color;
	[Export ("color", ArgumentSemantic.Assign)]
	ESTColor Color { get; }

	// @property (readonly, nonatomic, strong) NSString * hardwareVersion;
	[Export ("hardwareVersion", ArgumentSemantic.Strong)]
	string HardwareVersion { get; }

	// @property (readonly, nonatomic, strong) NSString * firmwareVersion;
	[Export ("firmwareVersion", ArgumentSemantic.Strong)]
	string FirmwareVersion { get; }

	// @property (readonly, assign, nonatomic) NSInteger rssi;
	[Export ("rssi", ArgumentSemantic.Assign)]
	nint Rssi { get; }

	// @property (readonly, nonatomic, strong) NSNumber * idleBatteryVoltage;
	[Export ("idleBatteryVoltage", ArgumentSemantic.Strong)]
	NSNumber IdleBatteryVoltage { get; }

	// @property (readonly, nonatomic, strong) NSNumber * stressBatteryVoltage;
	[Export ("stressBatteryVoltage", ArgumentSemantic.Strong)]
	NSNumber StressBatteryVoltage { get; }

	// @property (readonly, assign, nonatomic) unsigned long long currentMotionStateDuration;
	[Export ("currentMotionStateDuration")]
	ulong CurrentMotionStateDuration { get; }

	// @property (readonly, assign, nonatomic) unsigned long long previousMotionStateDuration;
	[Export ("previousMotionStateDuration")]
	ulong PreviousMotionStateDuration { get; }

	// @property (readonly, assign, nonatomic) BOOL isMoving;
	[Export ("isMoving")]
	bool IsMoving { get; }

	// @property (readonly, assign, nonatomic) ESTNearableOrientation orientation;
	[Export ("orientation", ArgumentSemantic.Assign)]
	ESTNearableOrientation Orientation { get; }

	// @property (readonly, assign, nonatomic) NSInteger xAcceleration;
	[Export ("xAcceleration", ArgumentSemantic.Assign)]
	nint XAcceleration { get; }

	// @property (readonly, assign, nonatomic) NSInteger yAcceleration;
	[Export ("yAcceleration", ArgumentSemantic.Assign)]
	nint YAcceleration { get; }

	// @property (readonly, assign, nonatomic) NSInteger zAcceleration;
	[Export ("zAcceleration", ArgumentSemantic.Assign)]
	nint ZAcceleration { get; }

	// @property (readonly, assign, nonatomic) double temperature;
	[Export ("temperature")]
	double Temperature { get; }

	// @property (readonly, nonatomic, strong) NSNumber * power;
	[Export ("power", ArgumentSemantic.Strong)]
	NSNumber Power { get; }

	// @property (readonly, assign, nonatomic) ESTNearableFirmwareState firmwareState;
	[Export ("firmwareState", ArgumentSemantic.Assign)]
	ESTNearableFirmwareState FirmwareState { get; }

	// -(CLBeaconRegion *)beaconRegion;
	[Export ("beaconRegion")]
	[Verify (MethodToProperty)]
	CLBeaconRegion BeaconRegion { get; }
}

// @protocol ESTTriggerDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface ESTTriggerDelegate
{
	// @optional -(void)triggerDidChangeState:(ESTTrigger *)trigger;
	[Export ("triggerDidChangeState:")]
	void TriggerDidChangeState (ESTTrigger trigger);
}

// @interface ESTTrigger : NSObject
[BaseType (typeof(NSObject))]
interface ESTTrigger
{
	[Wrap ("WeakDelegate")]
	ESTTriggerDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<ESTTriggerDelegate> delegate;
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @property (readonly, nonatomic, strong) NSArray * rules;
	[Export ("rules", ArgumentSemantic.Strong)]
	[Verify (StronglyTypedNSArray)]
	NSObject[] Rules { get; }

	// @property (readonly, nonatomic, strong) NSString * identifier;
	[Export ("identifier", ArgumentSemantic.Strong)]
	string Identifier { get; }

	// @property (readonly, assign, nonatomic) BOOL state;
	[Export ("state")]
	bool State { get; }

	// -(instancetype)initWithRules:(NSArray *)rules identifier:(NSString *)identifier;
	[Export ("initWithRules:identifier:")]
	[Verify (StronglyTypedNSArray)]
	IntPtr Constructor (NSObject[] rules, string identifier);
}

// @interface ESTRule : NSObject
[BaseType (typeof(NSObject))]
interface ESTRule
{
	// @property (assign, nonatomic) BOOL state;
	[Export ("state")]
	bool State { get; set; }

	// -(void)update;
	[Export ("update")]
	void Update ();
}

// @interface ESTDateRule : ESTRule
[BaseType (typeof(ESTRule))]
interface ESTDateRule
{
	// @property (nonatomic, strong) NSNumber * afterHour;
	[Export ("afterHour", ArgumentSemantic.Strong)]
	NSNumber AfterHour { get; set; }

	// @property (nonatomic, strong) NSNumber * beforeHour;
	[Export ("beforeHour", ArgumentSemantic.Strong)]
	NSNumber BeforeHour { get; set; }

	// +(instancetype)hourLaterThan:(int)hour;
	[Static]
	[Export ("hourLaterThan:")]
	ESTDateRule HourLaterThan (int hour);

	// +(instancetype)hourEarlierThan:(int)hour;
	[Static]
	[Export ("hourEarlierThan:")]
	ESTDateRule HourEarlierThan (int hour);

	// +(instancetype)hourBetween:(int)firstHour and:(int)secondHour;
	[Static]
	[Export ("hourBetween:and:")]
	ESTDateRule HourBetween (int firstHour, int secondHour);
}

// @interface ESTNearableRule : ESTRule
[BaseType (typeof(ESTRule))]
interface ESTNearableRule
{
	// @property (readonly, nonatomic, strong) NSString * nearableIdentifier;
	[Export ("nearableIdentifier", ArgumentSemantic.Strong)]
	string NearableIdentifier { get; }

	// @property (readonly, assign, nonatomic) ESTNearableType nearableType;
	[Export ("nearableType", ArgumentSemantic.Assign)]
	ESTNearableType NearableType { get; }

	// -(instancetype)initWithNearableIdentifier:(NSString *)identifier;
	[Export ("initWithNearableIdentifier:")]
	IntPtr Constructor (string identifier);

	// -(instancetype)initWithNearableType:(ESTNearableType)type;
	[Export ("initWithNearableType:")]
	IntPtr Constructor (ESTNearableType type);

	// -(void)updateWithNearable:(ESTNearable *)nearable;
	[Export ("updateWithNearable:")]
	void UpdateWithNearable (ESTNearable nearable);
}

// @interface ESTProximityRule : ESTNearableRule
[BaseType (typeof(ESTNearableRule))]
interface ESTProximityRule
{
	// @property (assign, nonatomic) BOOL inRange;
	[Export ("inRange")]
	bool InRange { get; set; }

	// +(instancetype)inRangeOfNearableIdentifier:(NSString *)identifier;
	[Static]
	[Export ("inRangeOfNearableIdentifier:")]
	ESTProximityRule InRangeOfNearableIdentifier (string identifier);

	// +(instancetype)inRangeOfNearableType:(ESTNearableType)type;
	[Static]
	[Export ("inRangeOfNearableType:")]
	ESTProximityRule InRangeOfNearableType (ESTNearableType type);

	// +(instancetype)outsideRangeOfNearableIdentifier:(NSString *)identifier;
	[Static]
	[Export ("outsideRangeOfNearableIdentifier:")]
	ESTProximityRule OutsideRangeOfNearableIdentifier (string identifier);

	// +(instancetype)outsideRangeOfNearableType:(ESTNearableType)type;
	[Static]
	[Export ("outsideRangeOfNearableType:")]
	ESTProximityRule OutsideRangeOfNearableType (ESTNearableType type);
}

// @interface ESTTemperatureRule : ESTNearableRule
[BaseType (typeof(ESTNearableRule))]
interface ESTTemperatureRule
{
	// @property (nonatomic, strong) NSNumber * maxValue;
	[Export ("maxValue", ArgumentSemantic.Strong)]
	NSNumber MaxValue { get; set; }

	// @property (nonatomic, strong) NSNumber * minValue;
	[Export ("minValue", ArgumentSemantic.Strong)]
	NSNumber MinValue { get; set; }

	// +(instancetype)temperatureGraterThan:(double)value forNearableIdentifier:(NSString *)identifier;
	[Static]
	[Export ("temperatureGraterThan:forNearableIdentifier:")]
	ESTTemperatureRule TemperatureGraterThan (double value, string identifier);

	// +(instancetype)temperatureLowerThan:(double)value forNearableIdentifier:(NSString *)identifier;
	[Static]
	[Export ("temperatureLowerThan:forNearableIdentifier:")]
	ESTTemperatureRule TemperatureLowerThan (double value, string identifier);

	// +(instancetype)temperatureBetween:(double)minValue and:(double)maxValue forNearableIdentifier:(NSString *)identifier;
	[Static]
	[Export ("temperatureBetween:and:forNearableIdentifier:")]
	ESTTemperatureRule TemperatureBetween (double minValue, double maxValue, string identifier);

	// +(instancetype)temperatureGraterThan:(double)value forNearableType:(ESTNearableType)type;
	[Static]
	[Export ("temperatureGraterThan:forNearableType:")]
	ESTTemperatureRule TemperatureGraterThan (double value, ESTNearableType type);

	// +(instancetype)temperatureLowerThan:(double)value forNearableType:(ESTNearableType)type;
	[Static]
	[Export ("temperatureLowerThan:forNearableType:")]
	ESTTemperatureRule TemperatureLowerThan (double value, ESTNearableType type);

	// +(instancetype)temperatureBetween:(double)minValue and:(double)maxValue forNearableType:(ESTNearableType)type;
	[Static]
	[Export ("temperatureBetween:and:forNearableType:")]
	ESTTemperatureRule TemperatureBetween (double minValue, double maxValue, ESTNearableType type);
}

// @interface ESTMotionRule : ESTNearableRule
[BaseType (typeof(ESTNearableRule))]
interface ESTMotionRule
{
	// @property (assign, nonatomic) BOOL motionState;
	[Export ("motionState")]
	bool MotionState { get; set; }

	// +(instancetype)motionStateEquals:(BOOL)motionState forNearableIdentifier:(NSString *)identifier;
	[Static]
	[Export ("motionStateEquals:forNearableIdentifier:")]
	ESTMotionRule MotionStateEquals (bool motionState, string identifier);

	// +(instancetype)motionStateEquals:(BOOL)motionState forNearableType:(ESTNearableType)type;
	[Static]
	[Export ("motionStateEquals:forNearableType:")]
	ESTMotionRule MotionStateEquals (bool motionState, ESTNearableType type);
}

// @interface ESTOrientationRule : ESTNearableRule
[BaseType (typeof(ESTNearableRule))]
interface ESTOrientationRule
{
	// @property (assign, nonatomic) ESTNearableOrientation orientation;
	[Export ("orientation", ArgumentSemantic.Assign)]
	ESTNearableOrientation Orientation { get; set; }

	// +(instancetype)orientationEquals:(ESTNearableOrientation)orientation forNearableIdentifier:(NSString *)identifier;
	[Static]
	[Export ("orientationEquals:forNearableIdentifier:")]
	ESTOrientationRule OrientationEquals (ESTNearableOrientation orientation, string identifier);

	// +(instancetype)orientationEquals:(ESTNearableOrientation)orientation forNearableType:(ESTNearableType)type;
	[Static]
	[Export ("orientationEquals:forNearableType:")]
	ESTOrientationRule OrientationEquals (ESTNearableOrientation orientation, ESTNearableType type);
}

// @protocol ESTTriggerManagerDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface ESTTriggerManagerDelegate
{
	// @optional -(void)triggerManager:(ESTTriggerManager *)manager triggerChangedState:(ESTTrigger *)trigger;
	[Export ("triggerManager:triggerChangedState:")]
	void TriggerChangedState (ESTTriggerManager manager, ESTTrigger trigger);
}

// @interface ESTTriggerManager : NSObject <ESTTriggerDelegate>
[BaseType (typeof(NSObject))]
interface ESTTriggerManager : IESTTriggerDelegate
{
	[Wrap ("WeakDelegate")]
	ESTTriggerManagerDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<ESTTriggerManagerDelegate> delegate;
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @property (readonly, nonatomic, strong) NSArray * triggers;
	[Export ("triggers", ArgumentSemantic.Strong)]
	[Verify (StronglyTypedNSArray)]
	NSObject[] Triggers { get; }

	// -(void)startMonitoringForTrigger:(ESTTrigger *)trigger;
	[Export ("startMonitoringForTrigger:")]
	void StartMonitoringForTrigger (ESTTrigger trigger);

	// -(void)stopMonitoringForTriggerWithIdentifier:(NSString *)identifier;
	[Export ("stopMonitoringForTriggerWithIdentifier:")]
	void StopMonitoringForTriggerWithIdentifier (string identifier);

	// -(BOOL)stateForTriggerWithIdentifier:(NSString *)identifier;
	[Export ("stateForTriggerWithIdentifier:")]
	bool StateForTriggerWithIdentifier (string identifier);
}

// @protocol ESTBeaconManagerDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface ESTBeaconManagerDelegate
{
	// @optional -(void)beaconManager:(id)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status;
	[Export ("beaconManager:didChangeAuthorizationStatus:")]
	void BeaconManager (NSObject manager, CLAuthorizationStatus status);

	// @optional -(void)beaconManager:(id)manager didStartMonitoringForRegion:(CLBeaconRegion *)region;
	[Export ("beaconManager:didStartMonitoringForRegion:")]
	void BeaconManager (NSObject manager, CLBeaconRegion region);

	// @optional -(void)beaconManager:(id)manager monitoringDidFailForRegion:(CLBeaconRegion *)region withError:(NSError *)error;
	[Export ("beaconManager:monitoringDidFailForRegion:withError:")]
	void BeaconManager (NSObject manager, CLBeaconRegion region, NSError error);

	// @optional -(void)beaconManager:(id)manager didEnterRegion:(CLBeaconRegion *)region;
	[Export ("beaconManager:didEnterRegion:")]
	void BeaconManager (NSObject manager, CLBeaconRegion region);

	// @optional -(void)beaconManager:(id)manager didExitRegion:(CLBeaconRegion *)region;
	[Export ("beaconManager:didExitRegion:")]
	void BeaconManager (NSObject manager, CLBeaconRegion region);

	// @optional -(void)beaconManager:(id)manager didDetermineState:(CLRegionState)state forRegion:(CLBeaconRegion *)region;
	[Export ("beaconManager:didDetermineState:forRegion:")]
	void BeaconManager (NSObject manager, CLRegionState state, CLBeaconRegion region);

	// @optional -(void)beaconManager:(id)manager didRangeBeacons:(NSArray *)beacons inRegion:(CLBeaconRegion *)region;
	[Export ("beaconManager:didRangeBeacons:inRegion:")]
	[Verify (StronglyTypedNSArray)]
	void BeaconManager (NSObject manager, NSObject[] beacons, CLBeaconRegion region);

	// @optional -(void)beaconManager:(id)manager rangingBeaconsDidFailForRegion:(CLBeaconRegion *)region withError:(NSError *)error;
	[Export ("beaconManager:rangingBeaconsDidFailForRegion:withError:")]
	void BeaconManager (NSObject manager, CLBeaconRegion region, NSError error);

	// @optional -(void)beaconManagerDidStartAdvertising:(id)manager error:(NSError *)error;
	[Export ("beaconManagerDidStartAdvertising:error:")]
	void BeaconManagerDidStartAdvertising (NSObject manager, NSError error);

	// @optional -(void)beaconManager:(id)manager didFailWithError:(NSError *)error;
	[Export ("beaconManager:didFailWithError:")]
	void BeaconManager (NSObject manager, NSError error);
}

// @interface ESTBeaconManager : NSObject
[BaseType (typeof(NSObject))]
interface ESTBeaconManager
{
	[Wrap ("WeakDelegate")]
	ESTBeaconManagerDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<ESTBeaconManagerDelegate> delegate;
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @property (nonatomic) NSInteger preventUnknownUpdateCount;
	[Export ("preventUnknownUpdateCount", ArgumentSemantic.Assign)]
	nint PreventUnknownUpdateCount { get; set; }

	// @property (nonatomic) BOOL avoidUnknownStateBeacons;
	[Export ("avoidUnknownStateBeacons")]
	bool AvoidUnknownStateBeacons { get; set; }

	// @property (nonatomic) BOOL returnAllRangedBeaconsAtOnce;
	[Export ("returnAllRangedBeaconsAtOnce")]
	bool ReturnAllRangedBeaconsAtOnce { get; set; }

	// -(void)updateRangeLimit:(NSInteger)limit;
	[Export ("updateRangeLimit:")]
	void UpdateRangeLimit (nint limit);

	// -(void)startAdvertisingWithProximityUUID:(NSUUID *)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor identifier:(NSString *)identifier;
	[Export ("startAdvertisingWithProximityUUID:major:minor:identifier:")]
	void StartAdvertisingWithProximityUUID (NSUUID proximityUUID, ushort major, ushort minor, string identifier);

	// -(void)stopAdvertising;
	[Export ("stopAdvertising")]
	void StopAdvertising ();

	// +(CLAuthorizationStatus)authorizationStatus;
	[Static]
	[Export ("authorizationStatus")]
	[Verify (MethodToProperty)]
	CLAuthorizationStatus AuthorizationStatus { get; }

	// -(void)requestWhenInUseAuthorization;
	[Export ("requestWhenInUseAuthorization")]
	void RequestWhenInUseAuthorization ();

	// -(void)requestAlwaysAuthorization;
	[Export ("requestAlwaysAuthorization")]
	void RequestAlwaysAuthorization ();

	// -(void)startMonitoringForRegion:(CLBeaconRegion *)region;
	[Export ("startMonitoringForRegion:")]
	void StartMonitoringForRegion (CLBeaconRegion region);

	// -(void)stopMonitoringForRegion:(CLBeaconRegion *)region;
	[Export ("stopMonitoringForRegion:")]
	void StopMonitoringForRegion (CLBeaconRegion region);

	// -(void)startRangingBeaconsInRegion:(CLBeaconRegion *)region;
	[Export ("startRangingBeaconsInRegion:")]
	void StartRangingBeaconsInRegion (CLBeaconRegion region);

	// -(void)stopRangingBeaconsInRegion:(CLBeaconRegion *)region;
	[Export ("stopRangingBeaconsInRegion:")]
	void StopRangingBeaconsInRegion (CLBeaconRegion region);

	// -(void)requestStateForRegion:(CLBeaconRegion *)region;
	[Export ("requestStateForRegion:")]
	void RequestStateForRegion (CLBeaconRegion region);

	// @property (readonly, copy, nonatomic) NSSet * monitoredRegions;
	[Export ("monitoredRegions", ArgumentSemantic.Copy)]
	NSSet MonitoredRegions { get; }

	// @property (readonly, copy, nonatomic) NSSet * rangedRegions;
	[Export ("rangedRegions", ArgumentSemantic.Copy)]
	NSSet RangedRegions { get; }

	// +(NSUUID *)motionProximityUUIDForProximityUUID:(NSUUID *)proximityUUID;
	[Static]
	[Export ("motionProximityUUIDForProximityUUID:")]
	NSUUID MotionProximityUUIDForProximityUUID (NSUUID proximityUUID);
}

// @interface ESTSecureBeaconManager : NSObject
[BaseType (typeof(NSObject))]
interface ESTSecureBeaconManager
{
	[Wrap ("WeakDelegate")]
	ESTBeaconManagerDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<ESTBeaconManagerDelegate> delegate;
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// +(CLAuthorizationStatus)authorizationStatus;
	[Static]
	[Export ("authorizationStatus")]
	[Verify (MethodToProperty)]
	CLAuthorizationStatus AuthorizationStatus { get; }

	// -(void)requestWhenInUseAuthorization;
	[Export ("requestWhenInUseAuthorization")]
	void RequestWhenInUseAuthorization ();

	// -(void)requestAlwaysAuthorization;
	[Export ("requestAlwaysAuthorization")]
	void RequestAlwaysAuthorization ();

	// -(void)startMonitoringForRegion:(CLBeaconRegion *)region;
	[Export ("startMonitoringForRegion:")]
	void StartMonitoringForRegion (CLBeaconRegion region);

	// -(void)stopMonitoringForRegion:(CLBeaconRegion *)region;
	[Export ("stopMonitoringForRegion:")]
	void StopMonitoringForRegion (CLBeaconRegion region);

	// -(void)startRangingBeaconsInRegion:(CLBeaconRegion *)region;
	[Export ("startRangingBeaconsInRegion:")]
	void StartRangingBeaconsInRegion (CLBeaconRegion region);

	// -(void)stopRangingBeaconsInRegion:(CLBeaconRegion *)region;
	[Export ("stopRangingBeaconsInRegion:")]
	void StopRangingBeaconsInRegion (CLBeaconRegion region);

	// -(void)requestStateForRegion:(CLBeaconRegion *)region;
	[Export ("requestStateForRegion:")]
	void RequestStateForRegion (CLBeaconRegion region);

	// @property (readonly, copy, nonatomic) NSSet * monitoredRegions;
	[Export ("monitoredRegions", ArgumentSemantic.Copy)]
	NSSet MonitoredRegions { get; }

	// @property (readonly, copy, nonatomic) NSSet * rangedRegions;
	[Export ("rangedRegions", ArgumentSemantic.Copy)]
	NSSet RangedRegions { get; }
}

// typedef void (^ESTPowerCompletionBlock)(ESTBeaconPowerNSError *);
delegate void ESTPowerCompletionBlock (ESTBeaconPower arg0, NSError arg1);

// @interface ESTBeaconDefinitions : NSObject
[BaseType (typeof(NSObject))]
interface ESTBeaconDefinitions
{
}

// @interface ESTBeaconVO : NSObject <NSCoding>
[BaseType (typeof(NSObject))]
interface ESTBeaconVO : INSCoding
{
	// @property (nonatomic, strong) NSString * proximityUUID;
	[Export ("proximityUUID", ArgumentSemantic.Strong)]
	string ProximityUUID { get; set; }

	// @property (nonatomic, strong) NSNumber * major;
	[Export ("major", ArgumentSemantic.Strong)]
	NSNumber Major { get; set; }

	// @property (nonatomic, strong) NSNumber * minor;
	[Export ("minor", ArgumentSemantic.Strong)]
	NSNumber Minor { get; set; }

	// @property (nonatomic, strong) NSString * macAddress;
	[Export ("macAddress", ArgumentSemantic.Strong)]
	string MacAddress { get; set; }

	// @property (assign, nonatomic) ESTBroadcastingScheme broadcastingScheme;
	[Export ("broadcastingScheme", ArgumentSemantic.Assign)]
	ESTBroadcastingScheme BroadcastingScheme { get; set; }

	// @property (nonatomic, strong) NSString * name;
	[Export ("name", ArgumentSemantic.Strong)]
	string Name { get; set; }

	// @property (nonatomic, strong) NSNumber * batteryLifeExpectancy;
	[Export ("batteryLifeExpectancy", ArgumentSemantic.Strong)]
	NSNumber BatteryLifeExpectancy { get; set; }

	// @property (nonatomic, strong) NSString * hardware;
	[Export ("hardware", ArgumentSemantic.Strong)]
	string Hardware { get; set; }

	// @property (nonatomic, strong) NSString * firmware;
	[Export ("firmware", ArgumentSemantic.Strong)]
	string Firmware { get; set; }

	// @property (assign, nonatomic) ESTBeaconPower power;
	[Export ("power", ArgumentSemantic.Assign)]
	ESTBeaconPower Power { get; set; }

	// @property (assign, nonatomic) NSInteger advInterval;
	[Export ("advInterval", ArgumentSemantic.Assign)]
	nint AdvInterval { get; set; }

	// @property (nonatomic, strong) NSNumber * basicPowerMode;
	[Export ("basicPowerMode", ArgumentSemantic.Strong)]
	NSNumber BasicPowerMode { get; set; }

	// @property (nonatomic, strong) NSNumber * smartPowerMode;
	[Export ("smartPowerMode", ArgumentSemantic.Strong)]
	NSNumber SmartPowerMode { get; set; }

	// @property (nonatomic, strong) NSNumber * batteryLevel;
	[Export ("batteryLevel", ArgumentSemantic.Strong)]
	NSNumber BatteryLevel { get; set; }

	// @property (nonatomic, strong) NSNumber * latitude;
	[Export ("latitude", ArgumentSemantic.Strong)]
	NSNumber Latitude { get; set; }

	// @property (nonatomic, strong) NSNumber * longitude;
	[Export ("longitude", ArgumentSemantic.Strong)]
	NSNumber Longitude { get; set; }

	// @property (nonatomic, strong) NSString * location;
	[Export ("location", ArgumentSemantic.Strong)]
	string Location { get; set; }

	// @property (nonatomic, strong) NSString * eddystoneNamespaceID;
	[Export ("eddystoneNamespaceID", ArgumentSemantic.Strong)]
	string EddystoneNamespaceID { get; set; }

	// @property (nonatomic, strong) NSString * eddystoneInstanceID;
	[Export ("eddystoneInstanceID", ArgumentSemantic.Strong)]
	string EddystoneInstanceID { get; set; }

	// @property (nonatomic, strong) NSString * eddystoneURL;
	[Export ("eddystoneURL", ArgumentSemantic.Strong)]
	string EddystoneURL { get; set; }

	// @property (nonatomic, strong) NSNumber * motionDetection;
	[Export ("motionDetection", ArgumentSemantic.Strong)]
	NSNumber MotionDetection { get; set; }

	// @property (assign, nonatomic) ESTBeaconConditionalBroadcasting conditionalBroadcasting;
	[Export ("conditionalBroadcasting", ArgumentSemantic.Assign)]
	ESTBeaconConditionalBroadcasting ConditionalBroadcasting { get; set; }

	// @property (assign, nonatomic) BOOL isSecured;
	[Export ("isSecured")]
	bool IsSecured { get; set; }

	// @property (nonatomic) ESTColor color;
	[Export ("color", ArgumentSemantic.Assign)]
	ESTColor Color { get; set; }

	// -(instancetype)initWithCloudData:(NSDictionary *)data;
	[Export ("initWithCloudData:")]
	IntPtr Constructor (NSDictionary data);
}

// @protocol ESTBeaconConnectionDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface ESTBeaconConnectionDelegate
{
	// @optional -(void)beaconConnection:(ESTBeaconConnection *)connection didVerifyWithData:(ESTBeaconVO *)data error:(NSError *)error;
	[Export ("beaconConnection:didVerifyWithData:error:")]
	void BeaconConnection (ESTBeaconConnection connection, ESTBeaconVO data, NSError error);

	// @optional -(void)beaconConnectionDidSucceed:(ESTBeaconConnection *)connection;
	[Export ("beaconConnectionDidSucceed:")]
	void BeaconConnectionDidSucceed (ESTBeaconConnection connection);

	// @optional -(void)beaconConnection:(ESTBeaconConnection *)connection didFailWithError:(NSError *)error;
	[Export ("beaconConnection:didFailWithError:")]
	void BeaconConnection (ESTBeaconConnection connection, NSError error);

	// @optional -(void)beaconConnection:(ESTBeaconConnection *)connection didDisconnectWithError:(NSError *)error;
	[Export ("beaconConnection:didDisconnectWithError:")]
	void BeaconConnection (ESTBeaconConnection connection, NSError error);

	// @optional -(void)beaconConnection:(ESTBeaconConnection *)connection motionStateChanged:(ESTBeaconMotionState)state;
	[Export ("beaconConnection:motionStateChanged:")]
	void BeaconConnection (ESTBeaconConnection connection, ESTBeaconMotionState state);

	// @optional -(void)beaconConnection:(ESTBeaconConnection *)connection didUpdateRSSI:(NSNumber *)rssi;
	[Export ("beaconConnection:didUpdateRSSI:")]
	void BeaconConnection (ESTBeaconConnection connection, NSNumber rssi);
}

// @interface ESTBeaconConnection : NSObject
[BaseType (typeof(NSObject))]
interface ESTBeaconConnection
{
	[Wrap ("WeakDelegate")]
	ESTBeaconConnectionDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<ESTBeaconConnectionDelegate> delegate;
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @property (readonly, nonatomic, strong) NSString * identifier;
	[Export ("identifier", ArgumentSemantic.Strong)]
	string Identifier { get; }

	// @property (readonly, nonatomic) ESTConnectionStatus connectionStatus;
	[Export ("connectionStatus")]
	ESTConnectionStatus ConnectionStatus { get; }

	// +(instancetype)connectionWithProximityUUID:(NSUUID *)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor delegate:(id<ESTBeaconConnectionDelegate>)delegate;
	[Static]
	[Export ("connectionWithProximityUUID:major:minor:delegate:")]
	ESTBeaconConnection ConnectionWithProximityUUID (NSUUID proximityUUID, ushort major, ushort minor, ESTBeaconConnectionDelegate @delegate);

	// +(instancetype)connectionWithBeacon:(CLBeacon *)beacon delegate:(id<ESTBeaconConnectionDelegate>)delegate;
	[Static]
	[Export ("connectionWithBeacon:delegate:")]
	ESTBeaconConnection ConnectionWithBeacon (CLBeacon beacon, ESTBeaconConnectionDelegate @delegate);

	// +(instancetype)connectionWithMacAddress:(NSString *)macAddress delegate:(id<ESTBeaconConnectionDelegate>)delegate;
	[Static]
	[Export ("connectionWithMacAddress:delegate:")]
	ESTBeaconConnection ConnectionWithMacAddress (string macAddress, ESTBeaconConnectionDelegate @delegate);

	// -(instancetype)initWithProximityUUID:(NSUUID *)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor delegate:(id<ESTBeaconConnectionDelegate>)delegate startImmediately:(BOOL)startImmediately;
	[Export ("initWithProximityUUID:major:minor:delegate:startImmediately:")]
	IntPtr Constructor (NSUUID proximityUUID, ushort major, ushort minor, ESTBeaconConnectionDelegate @delegate, bool startImmediately);

	// -(instancetype)initWithBeacon:(CLBeacon *)beacon delegate:(id<ESTBeaconConnectionDelegate>)delegate startImmediately:(BOOL)startImmediately;
	[Export ("initWithBeacon:delegate:startImmediately:")]
	IntPtr Constructor (CLBeacon beacon, ESTBeaconConnectionDelegate @delegate, bool startImmediately);

	// -(instancetype)initWithMacAddress:(NSString *)macAddress delegate:(id<ESTBeaconConnectionDelegate>)delegate startImmediately:(BOOL)startImmediately;
	[Export ("initWithMacAddress:delegate:startImmediately:")]
	IntPtr Constructor (string macAddress, ESTBeaconConnectionDelegate @delegate, bool startImmediately);

	// -(void)startConnection;
	[Export ("startConnection")]
	void StartConnection ();

	// -(void)startConnectionWithAttempts:(NSInteger)attempts connectionTimeout:(NSInteger)timeout;
	[Export ("startConnectionWithAttempts:connectionTimeout:")]
	void StartConnectionWithAttempts (nint attempts, nint timeout);

	// -(void)cancelConnection;
	[Export ("cancelConnection")]
	void CancelConnection ();

	// -(void)disconnect;
	[Export ("disconnect")]
	void Disconnect ();

	// @property (readonly, nonatomic) NSString * macAddress;
	[Export ("macAddress")]
	string MacAddress { get; }

	// @property (readonly, nonatomic) NSString * name;
	[Export ("name")]
	string Name { get; }

	// @property (readonly, nonatomic) ESTColor color;
	[Export ("color")]
	ESTColor Color { get; }

	// @property (readonly, nonatomic) CBPeripheral * peripheral;
	[Export ("peripheral")]
	CBPeripheral Peripheral { get; }

	// @property (readonly, nonatomic) ESTBroadcastingScheme broadcastingScheme;
	[Export ("broadcastingScheme")]
	ESTBroadcastingScheme BroadcastingScheme { get; }

	// @property (readonly, nonatomic) NSUUID * proximityUUID;
	[Export ("proximityUUID")]
	NSUUID ProximityUUID { get; }

	// @property (readonly, nonatomic) NSUUID * motionProximityUUID;
	[Export ("motionProximityUUID")]
	NSUUID MotionProximityUUID { get; }

	// @property (readonly, nonatomic) NSNumber * major;
	[Export ("major")]
	NSNumber Major { get; }

	// @property (readonly, nonatomic) NSNumber * minor;
	[Export ("minor")]
	NSNumber Minor { get; }

	// @property (readonly, nonatomic) NSNumber * power;
	[Export ("power")]
	NSNumber Power { get; }

	// @property (readonly, nonatomic) NSNumber * advInterval;
	[Export ("advInterval")]
	NSNumber AdvInterval { get; }

	// @property (readonly, nonatomic) NSString * eddystoneNamespace;
	[Export ("eddystoneNamespace")]
	string EddystoneNamespace { get; }

	// @property (readonly, nonatomic) NSString * eddystoneInstance;
	[Export ("eddystoneInstance")]
	string EddystoneInstance { get; }

	// @property (readonly, nonatomic) NSString * eddystoneURL;
	[Export ("eddystoneURL")]
	string EddystoneURL { get; }

	// @property (readonly, nonatomic) NSString * hardwareVersion;
	[Export ("hardwareVersion")]
	string HardwareVersion { get; }

	// @property (readonly, nonatomic) NSString * firmwareVersion;
	[Export ("firmwareVersion")]
	string FirmwareVersion { get; }

	// @property (readonly, nonatomic) NSNumber * rssi;
	[Export ("rssi")]
	NSNumber Rssi { get; }

	// @property (readonly, nonatomic) NSNumber * batteryLevel;
	[Export ("batteryLevel")]
	NSNumber BatteryLevel { get; }

	// @property (readonly, nonatomic) ESTBeaconBatteryType batteryType;
	[Export ("batteryType")]
	ESTBeaconBatteryType BatteryType { get; }

	// @property (readonly, nonatomic) NSNumber * remainingLifetime;
	[Export ("remainingLifetime")]
	NSNumber RemainingLifetime { get; }

	// @property (readonly, nonatomic) ESTBeaconPowerSavingMode basicPowerMode;
	[Export ("basicPowerMode")]
	ESTBeaconPowerSavingMode BasicPowerMode { get; }

	// @property (readonly, nonatomic) ESTBeaconPowerSavingMode smartPowerMode;
	[Export ("smartPowerMode")]
	ESTBeaconPowerSavingMode SmartPowerMode { get; }

	// @property (readonly, nonatomic) ESTBeaconEstimoteSecureUUID estimoteSecureUUIDState;
	[Export ("estimoteSecureUUIDState")]
	ESTBeaconEstimoteSecureUUID EstimoteSecureUUIDState { get; }

	// @property (readonly, nonatomic) ESTBeaconMotionUUID motionUUIDState;
	[Export ("motionUUIDState")]
	ESTBeaconMotionUUID MotionUUIDState { get; }

	// @property (readonly, nonatomic) ESTBeaconMotionState motionState;
	[Export ("motionState")]
	ESTBeaconMotionState MotionState { get; }

	// @property (readonly, nonatomic) ESTBeaconTemperatureState temperatureState;
	[Export ("temperatureState")]
	ESTBeaconTemperatureState TemperatureState { get; }

	// @property (readonly, nonatomic) ESTBeaconConditionalBroadcasting conditionalBroadcastingState;
	[Export ("conditionalBroadcastingState")]
	ESTBeaconConditionalBroadcasting ConditionalBroadcastingState { get; }

	// @property (readonly, nonatomic) ESTBeaconMotionDetection motionDetectionState;
	[Export ("motionDetectionState")]
	ESTBeaconMotionDetection MotionDetectionState { get; }

	// -(void)readTemperatureWithCompletion:(ESTNumberCompletionBlock)completion;
	[Export ("readTemperatureWithCompletion:")]
	void ReadTemperatureWithCompletion (ESTNumberCompletionBlock completion);

	// -(void)readAccelerometerCountWithCompletion:(ESTNumberCompletionBlock)completion;
	[Export ("readAccelerometerCountWithCompletion:")]
	void ReadAccelerometerCountWithCompletion (ESTNumberCompletionBlock completion);

	// -(void)resetAccelerometerCountWithCompletion:(ESTUnsignedShortCompletionBlock)completion;
	[Export ("resetAccelerometerCountWithCompletion:")]
	void ResetAccelerometerCountWithCompletion (ESTUnsignedShortCompletionBlock completion);

	// -(void)writeBroadcastingScheme:(ESTBroadcastingScheme)broadcastingScheme completion:(ESTUnsignedShortCompletionBlock)completion;
	[Export ("writeBroadcastingScheme:completion:")]
	void WriteBroadcastingScheme (ESTBroadcastingScheme broadcastingScheme, ESTUnsignedShortCompletionBlock completion);

	// -(void)writeConditionalBroadcastingType:(ESTBeaconConditionalBroadcasting)conditionalBroadcasting completion:(ESTBoolCompletionBlock)completion;
	[Export ("writeConditionalBroadcastingType:completion:")]
	void WriteConditionalBroadcastingType (ESTBeaconConditionalBroadcasting conditionalBroadcasting, ESTBoolCompletionBlock completion);

	// -(void)writeName:(NSString *)name completion:(ESTStringCompletionBlock)completion;
	[Export ("writeName:completion:")]
	void WriteName (string name, ESTStringCompletionBlock completion);

	// -(void)writeProximityUUID:(NSString *)pUUID completion:(ESTStringCompletionBlock)completion;
	[Export ("writeProximityUUID:completion:")]
	void WriteProximityUUID (string pUUID, ESTStringCompletionBlock completion);

	// -(void)writeMajor:(unsigned short)major completion:(ESTUnsignedShortCompletionBlock)completion;
	[Export ("writeMajor:completion:")]
	void WriteMajor (ushort major, ESTUnsignedShortCompletionBlock completion);

	// -(void)writeMinor:(unsigned short)minor completion:(ESTUnsignedShortCompletionBlock)completion;
	[Export ("writeMinor:completion:")]
	void WriteMinor (ushort minor, ESTUnsignedShortCompletionBlock completion);

	// -(void)writeAdvInterval:(unsigned short)interval completion:(ESTUnsignedShortCompletionBlock)completion;
	[Export ("writeAdvInterval:completion:")]
	void WriteAdvInterval (ushort interval, ESTUnsignedShortCompletionBlock completion);

	// -(void)writePower:(ESTBeaconPower)power completion:(ESTPowerCompletionBlock)completion;
	[Export ("writePower:completion:")]
	void WritePower (ESTBeaconPower power, ESTPowerCompletionBlock completion);

	// -(void)writeEddystoneDomainNamespace:(NSString *)eddystoneNamespace completion:(ESTStringCompletionBlock)completion;
	[Export ("writeEddystoneDomainNamespace:completion:")]
	void WriteEddystoneDomainNamespace (string eddystoneNamespace, ESTStringCompletionBlock completion);

	// -(void)writeEddystoneHexNamespace:(NSString *)eddystoneNamespace completion:(ESTStringCompletionBlock)completion;
	[Export ("writeEddystoneHexNamespace:completion:")]
	void WriteEddystoneHexNamespace (string eddystoneNamespace, ESTStringCompletionBlock completion);

	// -(void)writeEddystoneInstance:(NSString *)eddystoneInstance completion:(ESTStringCompletionBlock)completion;
	[Export ("writeEddystoneInstance:completion:")]
	void WriteEddystoneInstance (string eddystoneInstance, ESTStringCompletionBlock completion);

	// -(void)writeEddystoneURL:(NSString *)eddystoneURL completion:(ESTStringCompletionBlock)completion;
	[Export ("writeEddystoneURL:completion:")]
	void WriteEddystoneURL (string eddystoneURL, ESTStringCompletionBlock completion);

	// -(void)writeBasicPowerModeEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
	[Export ("writeBasicPowerModeEnabled:completion:")]
	void WriteBasicPowerModeEnabled (bool enable, ESTBoolCompletionBlock completion);

	// -(void)writeSmartPowerModeEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
	[Export ("writeSmartPowerModeEnabled:completion:")]
	void WriteSmartPowerModeEnabled (bool enable, ESTBoolCompletionBlock completion);

	// -(void)writeEstimoteSecureUUIDEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
	[Export ("writeEstimoteSecureUUIDEnabled:completion:")]
	void WriteEstimoteSecureUUIDEnabled (bool enable, ESTBoolCompletionBlock completion);

	// -(void)writeMotionDetectionEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
	[Export ("writeMotionDetectionEnabled:completion:")]
	void WriteMotionDetectionEnabled (bool enable, ESTBoolCompletionBlock completion);

	// -(void)writeMotionUUIDEnabled:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
	[Export ("writeMotionUUIDEnabled:completion:")]
	void WriteMotionUUIDEnabled (bool enable, ESTBoolCompletionBlock completion);

	// -(void)writeCalibratedTemperature:(NSNumber *)temperature completion:(ESTNumberCompletionBlock)completion;
	[Export ("writeCalibratedTemperature:completion:")]
	void WriteCalibratedTemperature (NSNumber temperature, ESTNumberCompletionBlock completion);

	// -(void)resetToFactorySettingsWithCompletion:(ESTCompletionBlock)completion;
	[Export ("resetToFactorySettingsWithCompletion:")]
	void ResetToFactorySettingsWithCompletion (ESTCompletionBlock completion);

	// -(void)getMacAddressWithCompletion:(ESTStringCompletionBlock)completion;
	[Export ("getMacAddressWithCompletion:")]
	void GetMacAddressWithCompletion (ESTStringCompletionBlock completion);

	// -(void)findPeripheralForBeaconWithTimeout:(NSUInteger)timeout completion:(ESTObjectCompletionBlock)completion;
	[Export ("findPeripheralForBeaconWithTimeout:completion:")]
	void FindPeripheralForBeaconWithTimeout (nuint timeout, ESTObjectCompletionBlock completion);

	// -(void)checkFirmwareUpdateWithCompletion:(ESTObjectCompletionBlock)completion;
	[Export ("checkFirmwareUpdateWithCompletion:")]
	void CheckFirmwareUpdateWithCompletion (ESTObjectCompletionBlock completion);

	// -(void)updateFirmwareWithProgress:(ESTProgressBlock)progress completion:(ESTCompletionBlock)completion;
	[Export ("updateFirmwareWithProgress:completion:")]
	void UpdateFirmwareWithProgress (ESTProgressBlock progress, ESTCompletionBlock completion);
}

// @interface ESTBluetoothBeacon : NSObject
[BaseType (typeof(NSObject))]
interface ESTBluetoothBeacon
{
	// @property (nonatomic, strong) NSString * macAddress;
	[Export ("macAddress", ArgumentSemantic.Strong)]
	string MacAddress { get; set; }

	// @property (nonatomic, strong) NSNumber * major;
	[Export ("major", ArgumentSemantic.Strong)]
	NSNumber Major { get; set; }

	// @property (nonatomic, strong) NSNumber * minor;
	[Export ("minor", ArgumentSemantic.Strong)]
	NSNumber Minor { get; set; }

	// @property (nonatomic, strong) CBPeripheral * peripheral;
	[Export ("peripheral", ArgumentSemantic.Strong)]
	CBPeripheral Peripheral { get; set; }

	// @property (nonatomic, strong) NSNumber * measuredPower;
	[Export ("measuredPower", ArgumentSemantic.Strong)]
	NSNumber MeasuredPower { get; set; }

	// @property (nonatomic, strong) NSDate * discoveryDate;
	[Export ("discoveryDate", ArgumentSemantic.Strong)]
	NSDate DiscoveryDate { get; set; }

	// @property (nonatomic, strong) NSData * advertisementData;
	[Export ("advertisementData", ArgumentSemantic.Strong)]
	NSData AdvertisementData { get; set; }

	// @property (assign, nonatomic) NSInteger rssi;
	[Export ("rssi", ArgumentSemantic.Assign)]
	nint Rssi { get; set; }

	// @property (assign, nonatomic) NSInteger firmwareState;
	[Export ("firmwareState", ArgumentSemantic.Assign)]
	nint FirmwareState { get; set; }
}

// @protocol ESTUtilityManagerDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface ESTUtilityManagerDelegate
{
	// @optional -(void)utilityManager:(ESTUtilityManager *)manager didDiscoverBeacons:(NSArray *)beacons;
	[Export ("utilityManager:didDiscoverBeacons:")]
	[Verify (StronglyTypedNSArray)]
	void UtilityManager (ESTUtilityManager manager, NSObject[] beacons);

	// @optional -(void)utilityManagerDidFailDiscovery:(ESTUtilityManager *)manager;
	[Export ("utilityManagerDidFailDiscovery:")]
	void UtilityManagerDidFailDiscovery (ESTUtilityManager manager);
}

// @interface ESTUtilityManager : NSObject
[BaseType (typeof(NSObject))]
interface ESTUtilityManager
{
	// @property (readonly, assign, nonatomic) ESTUtilitManagerState state;
	[Export ("state", ArgumentSemantic.Assign)]
	ESTUtilitManagerState State { get; }

	[Wrap ("WeakDelegate")]
	ESTUtilityManagerDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<ESTUtilityManagerDelegate> delegate;
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// -(void)startEstimoteBeaconDiscovery;
	[Export ("startEstimoteBeaconDiscovery")]
	void StartEstimoteBeaconDiscovery ();

	// -(void)startEstimoteBeaconDiscoveryWithUpdateInterval:(NSTimeInterval)interval;
	[Export ("startEstimoteBeaconDiscoveryWithUpdateInterval:")]
	void StartEstimoteBeaconDiscoveryWithUpdateInterval (double interval);

	// -(void)stopEstimoteBeaconDiscovery;
	[Export ("stopEstimoteBeaconDiscovery")]
	void StopEstimoteBeaconDiscovery ();
}

// @protocol ESTNearableManagerDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface ESTNearableManagerDelegate
{
	// @optional -(void)nearableManager:(ESTNearableManager *)manager didRangeNearables:(NSArray *)nearables withType:(ESTNearableType)type;
	[Export ("nearableManager:didRangeNearables:withType:")]
	[Verify (StronglyTypedNSArray)]
	void DidRangeNearables (ESTNearableManager manager, NSObject[] nearables, ESTNearableType type);

	// @optional -(void)nearableManager:(ESTNearableManager *)manager didRangeNearable:(ESTNearable *)nearable;
	[Export ("nearableManager:didRangeNearable:")]
	void DidRangeNearable (ESTNearableManager manager, ESTNearable nearable);

	// @optional -(void)nearableManager:(ESTNearableManager *)manager rangingFailedWithError:(NSError *)error;
	[Export ("nearableManager:rangingFailedWithError:")]
	void RangingFailedWithError (ESTNearableManager manager, NSError error);

	// @optional -(void)nearableManager:(ESTNearableManager *)manager didEnterTypeRegion:(ESTNearableType)type;
	[Export ("nearableManager:didEnterTypeRegion:")]
	void DidEnterTypeRegion (ESTNearableManager manager, ESTNearableType type);

	// @optional -(void)nearableManager:(ESTNearableManager *)manager didExitTypeRegion:(ESTNearableType)type;
	[Export ("nearableManager:didExitTypeRegion:")]
	void DidExitTypeRegion (ESTNearableManager manager, ESTNearableType type);

	// @optional -(void)nearableManager:(ESTNearableManager *)manager didEnterIdentifierRegion:(NSString *)identifier;
	[Export ("nearableManager:didEnterIdentifierRegion:")]
	void DidEnterIdentifierRegion (ESTNearableManager manager, string identifier);

	// @optional -(void)nearableManager:(ESTNearableManager *)manager didExitIdentifierRegion:(NSString *)identifier;
	[Export ("nearableManager:didExitIdentifierRegion:")]
	void DidExitIdentifierRegion (ESTNearableManager manager, string identifier);

	// @optional -(void)nearableManager:(ESTNearableManager *)manager monitoringFailedWithError:(NSError *)error;
	[Export ("nearableManager:monitoringFailedWithError:")]
	void MonitoringFailedWithError (ESTNearableManager manager, NSError error);
}

// @interface ESTNearableManager : NSObject
[BaseType (typeof(NSObject))]
interface ESTNearableManager
{
	[Wrap ("WeakDelegate")]
	ESTNearableManagerDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<ESTNearableManagerDelegate> delegate;
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// -(void)startMonitoringForIdentifier:(NSString *)identifier;
	[Export ("startMonitoringForIdentifier:")]
	void StartMonitoringForIdentifier (string identifier);

	// -(void)stopMonitoringForIdentifier:(NSString *)identifier;
	[Export ("stopMonitoringForIdentifier:")]
	void StopMonitoringForIdentifier (string identifier);

	// -(void)startMonitoringForType:(ESTNearableType)type;
	[Export ("startMonitoringForType:")]
	void StartMonitoringForType (ESTNearableType type);

	// -(void)stopMonitoringForType:(ESTNearableType)type;
	[Export ("stopMonitoringForType:")]
	void StopMonitoringForType (ESTNearableType type);

	// -(void)stopMonitoring;
	[Export ("stopMonitoring")]
	void StopMonitoring ();

	// -(void)startRangingForIdentifier:(NSString *)identifier;
	[Export ("startRangingForIdentifier:")]
	void StartRangingForIdentifier (string identifier);

	// -(void)stopRangingForIdentifier:(NSString *)identifier;
	[Export ("stopRangingForIdentifier:")]
	void StopRangingForIdentifier (string identifier);

	// -(void)startRangingForType:(ESTNearableType)type;
	[Export ("startRangingForType:")]
	void StartRangingForType (ESTNearableType type);

	// -(void)stopRangingForType:(ESTNearableType)type;
	[Export ("stopRangingForType:")]
	void StopRangingForType (ESTNearableType type);

	// -(void)stopRanging;
	[Export ("stopRanging")]
	void StopRanging ();
}

// @interface ESTSimulatedNearableManager : ESTNearableManager <ESTNearableManagerDelegate>
[BaseType (typeof(ESTNearableManager))]
interface ESTSimulatedNearableManager : IESTNearableManagerDelegate
{
	// @property (readonly, nonatomic, strong) NSMutableArray * nearables;
	[Export ("nearables", ArgumentSemantic.Strong)]
	NSMutableArray Nearables { get; }

	// -(instancetype)initWithDelegate:(id<ESTNearableManagerDelegate>)delegate;
	[Export ("initWithDelegate:")]
	IntPtr Constructor (ESTNearableManagerDelegate @delegate);

	// -(instancetype)initWithDelegate:(id<ESTNearableManagerDelegate>)delegate pathForJSON:(NSString *)path;
	[Export ("initWithDelegate:pathForJSON:")]
	IntPtr Constructor (ESTNearableManagerDelegate @delegate, string path);

	// -(ESTNearable *)generateRandomNearableAndAddToSimulator:(BOOL)add;
	[Export ("generateRandomNearableAndAddToSimulator:")]
	ESTNearable GenerateRandomNearableAndAddToSimulator (bool add);

	// -(void)addNearableToSimulation:(NSString *)identifier withType:(ESTNearableType)type zone:(ESTNearableZone)zone rssi:(NSInteger)rssi;
	[Export ("addNearableToSimulation:withType:zone:rssi:")]
	void AddNearableToSimulation (string identifier, ESTNearableType type, ESTNearableZone zone, nint rssi);

	// -(void)addNearablesToSimulationWithPath:(NSString *)path;
	[Export ("addNearablesToSimulationWithPath:")]
	void AddNearablesToSimulationWithPath (string path);

	// -(void)simulateZone:(ESTNearableZone)zone forNearable:(NSString *)identifier;
	[Export ("simulateZone:forNearable:")]
	void SimulateZone (ESTNearableZone zone, string identifier);

	// -(void)simulateDidEnterRegionForNearable:(ESTNearable *)nearable;
	[Export ("simulateDidEnterRegionForNearable:")]
	void SimulateDidEnterRegionForNearable (ESTNearable nearable);

	// -(void)simulateDidExitRegionForNearable:(ESTNearable *)nearable;
	[Export ("simulateDidExitRegionForNearable:")]
	void SimulateDidExitRegionForNearable (ESTNearable nearable);
}

// @interface ESTCloudManager : NSObject
[BaseType (typeof(NSObject))]
interface ESTCloudManager
{
	// +(void)setupAppID:(NSString *)appID andAppToken:(NSString *)appToken;
	[Static]
	[Export ("setupAppID:andAppToken:")]
	void SetupAppID (string appID, string appToken);

	// +(BOOL)isAuthorized;
	[Static]
	[Export ("isAuthorized")]
	[Verify (MethodToProperty)]
	bool IsAuthorized { get; }

	// +(void)enableAnalytics:(BOOL)enable __attribute__((deprecated("Starting from SDK 3.2.0 use enableMonitoringAnalytics: or enableRangingAnalytics: instead.")));
	[Static]
	[Export ("enableAnalytics:")]
	void EnableAnalytics (bool enable);

	// +(void)enableMonitoringAnalytics:(BOOL)enable;
	[Static]
	[Export ("enableMonitoringAnalytics:")]
	void EnableMonitoringAnalytics (bool enable);

	// +(void)enableRangingAnalytics:(BOOL)enable;
	[Static]
	[Export ("enableRangingAnalytics:")]
	void EnableRangingAnalytics (bool enable);

	// +(void)enableGPSPositioningForAnalytics:(BOOL)enable;
	[Static]
	[Export ("enableGPSPositioningForAnalytics:")]
	void EnableGPSPositioningForAnalytics (bool enable);

	// +(BOOL)isAnalyticsEnabled __attribute__((deprecated("Starting from SDK 3.2.0 use enableMonitoringAnalytics: or enableRangingAnalytics: instead.")));
	[Static]
	[Export ("isAnalyticsEnabled")]
	[Verify (MethodToProperty)]
	bool IsAnalyticsEnabled { get; }

	// +(BOOL)isMonitoringAnalyticsEnabled;
	[Static]
	[Export ("isMonitoringAnalyticsEnabled")]
	[Verify (MethodToProperty)]
	bool IsMonitoringAnalyticsEnabled { get; }

	// +(BOOL)isRangingAnalyticsEnabled;
	[Static]
	[Export ("isRangingAnalyticsEnabled")]
	[Verify (MethodToProperty)]
	bool IsRangingAnalyticsEnabled { get; }

	// -(void)fetchEstimoteNearablesWithCompletion:(ESTArrayCompletionBlock)completion;
	[Export ("fetchEstimoteNearablesWithCompletion:")]
	void FetchEstimoteNearablesWithCompletion (ESTArrayCompletionBlock completion);

	// -(void)fetchEstimoteBeaconsWithCompletion:(ESTArrayCompletionBlock)completion;
	[Export ("fetchEstimoteBeaconsWithCompletion:")]
	void FetchEstimoteBeaconsWithCompletion (ESTArrayCompletionBlock completion);

	// -(void)fetchBeaconDetails:(NSString *)beaconUID completion:(ESTObjectCompletionBlock)completion;
	[Export ("fetchBeaconDetails:completion:")]
	void FetchBeaconDetails (string beaconUID, ESTObjectCompletionBlock completion);

	// -(void)fetchColorForBeacon:(CLBeacon *)beacon completion:(ESTObjectCompletionBlock)completion;
	[Export ("fetchColorForBeacon:completion:")]
	void FetchColorForBeacon (CLBeacon beacon, ESTObjectCompletionBlock completion);

	// -(void)fetchColorForBeaconWithProximityUUID:(NSUUID *)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor completion:(ESTObjectCompletionBlock)completion;
	[Export ("fetchColorForBeaconWithProximityUUID:major:minor:completion:")]
	void FetchColorForBeaconWithProximityUUID (NSUUID proximityUUID, ushort major, ushort minor, ESTObjectCompletionBlock completion);

	// -(void)fetchColorForBeaconWithMacAddress:(NSString *)macAddress completion:(ESTObjectCompletionBlock)completion;
	[Export ("fetchColorForBeaconWithMacAddress:completion:")]
	void FetchColorForBeaconWithMacAddress (string macAddress, ESTObjectCompletionBlock completion);

	// -(void)fetchMacAddressForBeacon:(CLBeacon *)beacon completion:(ESTStringCompletionBlock)completion;
	[Export ("fetchMacAddressForBeacon:completion:")]
	void FetchMacAddressForBeacon (CLBeacon beacon, ESTStringCompletionBlock completion);

	// -(void)assignGPSLocation:(CLLocation *)location toBeacon:(CLBeacon *)beacon completion:(ESTObjectCompletionBlock)completion;
	[Export ("assignGPSLocation:toBeacon:completion:")]
	void AssignGPSLocation (CLLocation location, CLBeacon beacon, ESTObjectCompletionBlock completion);

	// -(void)assignCurrentGPSLocationToBeacon:(CLBeacon *)beacon completion:(ESTObjectCompletionBlock)completion;
	[Export ("assignCurrentGPSLocationToBeacon:completion:")]
	void AssignCurrentGPSLocationToBeacon (CLBeacon beacon, ESTObjectCompletionBlock completion);

	// -(void)registerDeviceForRemoteManagement:(NSData *)deviceToken completion:(ESTCompletionBlock)completion;
	[Export ("registerDeviceForRemoteManagement:completion:")]
	void RegisterDeviceForRemoteManagement (NSData deviceToken, ESTCompletionBlock completion);

	// -(void)fetchPendingBeaconsSettingsWithCompletion:(ESTArrayCompletionBlock)completion;
	[Export ("fetchPendingBeaconsSettingsWithCompletion:")]
	void FetchPendingBeaconsSettingsWithCompletion (ESTArrayCompletionBlock completion);
}

// @interface ESTNotificationTransporter : NSObject
[BaseType (typeof(NSObject))]
interface ESTNotificationTransporter
{
	// @property (readonly, nonatomic) NSString * currentAppGroupIdentifier;
	[Export ("currentAppGroupIdentifier")]
	string CurrentAppGroupIdentifier { get; }

	// +(instancetype)sharedTransporter;
	[Static]
	[Export ("sharedTransporter")]
	ESTNotificationTransporter SharedTransporter ();

	// -(void)setAppGroupIdentifier:(NSString *)identifier;
	[Export ("setAppGroupIdentifier:")]
	void SetAppGroupIdentifier (string identifier);

	// -(BOOL)saveNearableZoneDescription:(NSString *)zone;
	[Export ("saveNearableZoneDescription:")]
	bool SaveNearableZoneDescription (string zone);

	// -(NSString *)readNearableZoneDescription;
	[Export ("readNearableZoneDescription")]
	[Verify (MethodToProperty)]
	string ReadNearableZoneDescription { get; }

	// -(BOOL)saveNearable:(ESTNearable *)nearable;
	[Export ("saveNearable:")]
	bool SaveNearable (ESTNearable nearable);

	// -(ESTNearable *)readNearable;
	[Export ("readNearable")]
	[Verify (MethodToProperty)]
	ESTNearable ReadNearable { get; }

	// -(BOOL)didRangeNearables:(NSArray *)nearables;
	[Export ("didRangeNearables:")]
	[Verify (StronglyTypedNSArray)]
	bool DidRangeNearables (NSObject[] nearables);

	// -(NSArray *)readRangedNearables;
	[Export ("readRangedNearables")]
	[Verify (MethodToProperty), Verify (StronglyTypedNSArray)]
	NSObject[] ReadRangedNearables { get; }

	// -(void)notifyDidEnterRegion:(CLBeaconRegion *)region;
	[Export ("notifyDidEnterRegion:")]
	void NotifyDidEnterRegion (CLBeaconRegion region);

	// -(void)notifyDidExitRegion:(CLBeaconRegion *)region;
	[Export ("notifyDidExitRegion:")]
	void NotifyDidExitRegion (CLBeaconRegion region);

	// -(void)notifyDidEnterIdentifierRegion:(NSString *)identifier;
	[Export ("notifyDidEnterIdentifierRegion:")]
	void NotifyDidEnterIdentifierRegion (string identifier);

	// -(void)notifyDidExitIdentifierRegion:(NSString *)identifier;
	[Export ("notifyDidExitIdentifierRegion:")]
	void NotifyDidExitIdentifierRegion (string identifier);

	// -(NSString *)readIdentifierForMonitoringEvents;
	[Export ("readIdentifierForMonitoringEvents")]
	[Verify (MethodToProperty)]
	string ReadIdentifierForMonitoringEvents { get; }

	// -(void)addObserver:(id)observer selector:(SEL)selector forNotification:(ESTNotification)notification;
	[Export ("addObserver:selector:forNotification:")]
	void AddObserver (NSObject observer, Selector selector, ESTNotification notification);

	// -(void)removeObserver:(id)observer;
	[Export ("removeObserver:")]
	void RemoveObserver (NSObject observer);
}

// @interface ESTBeaconUpdateConfig : NSObject <NSCoding, NSCopying>
[BaseType (typeof(NSObject))]
interface ESTBeaconUpdateConfig : INSCoding, INSCopying
{
	// @property (nonatomic, strong) NSString * proximityUUID;
	[Export ("proximityUUID", ArgumentSemantic.Strong)]
	string ProximityUUID { get; set; }

	// @property (nonatomic, strong) NSNumber * major;
	[Export ("major", ArgumentSemantic.Strong)]
	NSNumber Major { get; set; }

	// @property (nonatomic, strong) NSNumber * minor;
	[Export ("minor", ArgumentSemantic.Strong)]
	NSNumber Minor { get; set; }

	// @property (nonatomic, strong) NSNumber * advInterval;
	[Export ("advInterval", ArgumentSemantic.Strong)]
	NSNumber AdvInterval { get; set; }

	// @property (nonatomic, strong) NSNumber * power;
	[Export ("power", ArgumentSemantic.Strong)]
	NSNumber Power { get; set; }

	// @property (nonatomic, strong) NSNumber * basicPowerMode;
	[Export ("basicPowerMode", ArgumentSemantic.Strong)]
	NSNumber BasicPowerMode { get; set; }

	// @property (nonatomic, strong) NSNumber * smartPowerMode;
	[Export ("smartPowerMode", ArgumentSemantic.Strong)]
	NSNumber SmartPowerMode { get; set; }

	// @property (nonatomic, strong) NSNumber * estimoteSecureUUIDState;
	[Export ("estimoteSecureUUIDState", ArgumentSemantic.Strong)]
	NSNumber EstimoteSecureUUIDState { get; set; }
}

// @protocol ESBeaconUpdateInfoDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface ESBeaconUpdateInfoDelegate
{
	// @required -(void)beaconUpdateInfoInitialized:(id)beaconUpdateInfo;
	[Abstract]
	[Export ("beaconUpdateInfoInitialized:")]
	void BeaconUpdateInfoInitialized (NSObject beaconUpdateInfo);
}

// @interface ESTBeaconUpdateInfo : NSObject <NSCoding>
[BaseType (typeof(NSObject))]
interface ESTBeaconUpdateInfo : INSCoding
{
	[Wrap ("WeakDelegate")]
	ESBeaconUpdateInfoDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<ESBeaconUpdateInfoDelegate> delegate;
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @property (nonatomic, strong) ESTBeaconConnection * beaconConnection;
	[Export ("beaconConnection", ArgumentSemantic.Strong)]
	ESTBeaconConnection BeaconConnection { get; set; }

	// @property (nonatomic, strong) NSString * macAddress;
	[Export ("macAddress", ArgumentSemantic.Strong)]
	string MacAddress { get; set; }

	// @property (nonatomic, strong) ESTBeaconUpdateConfig * config;
	[Export ("config", ArgumentSemantic.Strong)]
	ESTBeaconUpdateConfig Config { get; set; }

	// @property (assign, nonatomic) ESBeaconUpdateInfoStatus status;
	[Export ("status", ArgumentSemantic.Assign)]
	ESBeaconUpdateInfoStatus Status { get; set; }

	// @property (nonatomic, strong) NSError * error;
	[Export ("error", ArgumentSemantic.Strong)]
	NSError Error { get; set; }

	// -(instancetype)initWithMacAddress:(NSString *)macAddress config:(ESTBeaconUpdateConfig *)config;
	[Export ("initWithMacAddress:config:")]
	IntPtr Constructor (string macAddress, ESTBeaconUpdateConfig config);

	// -(instancetype)initWithMacAddress:(NSString *)macAddress config:(ESTBeaconUpdateConfig *)config delegate:(id<ESBeaconUpdateInfoDelegate>)delegate __attribute__((objc_designated_initializer));
	[Export ("initWithMacAddress:config:delegate:")]
	IntPtr Constructor (string macAddress, ESTBeaconUpdateConfig config, ESBeaconUpdateInfoDelegate @delegate);

	// -(void)findPeripheralWithTimeout:(NSTimeInterval)timeout;
	[Export ("findPeripheralWithTimeout:")]
	void FindPeripheralWithTimeout (double timeout);

	// -(void)updateWithConfig:(ESTBeaconUpdateConfig *)config;
	[Export ("updateWithConfig:")]
	void UpdateWithConfig (ESTBeaconUpdateConfig config);

	// -(NSString *)description;
	[Export ("description")]
	[Verify (MethodToProperty)]
	string Description { get; }
}

[Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern NSString *const ESTBulkUpdaterBeginNotification;
	[Field ("ESTBulkUpdaterBeginNotification")]
	NSString ESTBulkUpdaterBeginNotification { get; }

	// extern NSString *const ESTBulkUpdaterProgressNotification;
	[Field ("ESTBulkUpdaterProgressNotification")]
	NSString ESTBulkUpdaterProgressNotification { get; }

	// extern NSString *const ESTBulkUpdaterCompleteNotification;
	[Field ("ESTBulkUpdaterCompleteNotification")]
	NSString ESTBulkUpdaterCompleteNotification { get; }

	// extern NSString *const ESTBulkUpdaterFailNotification;
	[Field ("ESTBulkUpdaterFailNotification")]
	NSString ESTBulkUpdaterFailNotification { get; }

	// extern NSString *const ESTBulkUpdaterTimeoutNotification;
	[Field ("ESTBulkUpdaterTimeoutNotification")]
	NSString ESTBulkUpdaterTimeoutNotification { get; }
}

// @interface ESTBulkUpdater : NSObject
[BaseType (typeof(NSObject))]
interface ESTBulkUpdater
{
	// @property (nonatomic, strong) NSArray * beaconInfos;
	[Export ("beaconInfos", ArgumentSemantic.Strong)]
	[Verify (StronglyTypedNSArray)]
	NSObject[] BeaconInfos { get; set; }

	// @property (readonly, nonatomic) ESTBulkUpdaterMode mode;
	[Export ("mode")]
	ESTBulkUpdaterMode Mode { get; }

	// @property (readonly, nonatomic) ESBulkUpdaterStatus status;
	[Export ("status")]
	ESBulkUpdaterStatus Status { get; }

	// +(ESTBulkUpdater *)sharedInstance;
	[Static]
	[Export ("sharedInstance")]
	[Verify (MethodToProperty)]
	ESTBulkUpdater SharedInstance { get; }

	// +(BOOL)verifyPushNotificationPayload:(NSDictionary *)payload;
	[Static]
	[Export ("verifyPushNotificationPayload:")]
	bool VerifyPushNotificationPayload (NSDictionary payload);

	// -(void)startWithCloudSettingsAndTimeout:(NSTimeInterval)timeout;
	[Export ("startWithCloudSettingsAndTimeout:")]
	void StartWithCloudSettingsAndTimeout (double timeout);

	// -(void)startWithBeaconInfos:(NSArray *)beaconInfos timeout:(NSTimeInterval)timeout;
	[Export ("startWithBeaconInfos:timeout:")]
	[Verify (StronglyTypedNSArray)]
	void StartWithBeaconInfos (NSObject[] beaconInfos, double timeout);

	// -(BOOL)isUpdateInProgressForBeaconWithMacAddress:(NSString *)macAddress;
	[Export ("isUpdateInProgressForBeaconWithMacAddress:")]
	bool IsUpdateInProgressForBeaconWithMacAddress (string macAddress);

	// -(BOOL)isBeaconWaitingForUpdate:(NSString *)macAddress;
	[Export ("isBeaconWaitingForUpdate:")]
	bool IsBeaconWaitingForUpdate (string macAddress);

	// -(NSArray *)getBeaconUpdateInfosForBeaconWithMacAddress:(NSString *)macAddress;
	[Export ("getBeaconUpdateInfosForBeaconWithMacAddress:")]
	[Verify (StronglyTypedNSArray)]
	NSObject[] GetBeaconUpdateInfosForBeaconWithMacAddress (string macAddress);

	// -(NSTimeInterval)getTimeLeftToTimeout;
	[Export ("getTimeLeftToTimeout")]
	[Verify (MethodToProperty)]
	double TimeLeftToTimeout { get; }

	// -(void)cancel;
	[Export ("cancel")]
	void Cancel ();
}

// @interface ESTEddystoneTelemetry : NSObject <NSCopying>
[BaseType (typeof(NSObject))]
interface ESTEddystoneTelemetry : INSCopying
{
	// @property (nonatomic, strong) NSNumber * battery;
	[Export ("battery", ArgumentSemantic.Strong)]
	NSNumber Battery { get; set; }

	// @property (nonatomic, strong) NSNumber * batteryVoltage;
	[Export ("batteryVoltage", ArgumentSemantic.Strong)]
	NSNumber BatteryVoltage { get; set; }

	// @property (nonatomic, strong) NSNumber * temperature;
	[Export ("temperature", ArgumentSemantic.Strong)]
	NSNumber Temperature { get; set; }

	// @property (nonatomic, strong) NSNumber * packetCount;
	[Export ("packetCount", ArgumentSemantic.Strong)]
	NSNumber PacketCount { get; set; }

	// @property (nonatomic, strong) NSNumber * uptimeMillis;
	[Export ("uptimeMillis", ArgumentSemantic.Strong)]
	NSNumber UptimeMillis { get; set; }

	// @property (nonatomic, strong) NSNumber * awakeTime;
	[Export ("awakeTime", ArgumentSemantic.Strong)]
	NSNumber AwakeTime { get; set; }
}

// @interface ESTEddystone : NSObject <NSCopying>
[BaseType (typeof(NSObject))]
interface ESTEddystone : INSCopying
{
	// @property (nonatomic, strong) NSString * macAddress;
	[Export ("macAddress", ArgumentSemantic.Strong)]
	string MacAddress { get; set; }

	// @property (nonatomic, strong) NSNumber * rssi;
	[Export ("rssi", ArgumentSemantic.Strong)]
	NSNumber Rssi { get; set; }

	// @property (nonatomic, strong) NSNumber * accuracy;
	[Export ("accuracy", ArgumentSemantic.Strong)]
	NSNumber Accuracy { get; set; }

	// @property (nonatomic) ESTEddystoneProximity proximity;
	[Export ("proximity", ArgumentSemantic.Assign)]
	ESTEddystoneProximity Proximity { get; set; }

	// @property (nonatomic, strong) NSDate * discoveryDate;
	[Export ("discoveryDate", ArgumentSemantic.Strong)]
	NSDate DiscoveryDate { get; set; }

	// @property (nonatomic, strong) NSNumber * measuredPower;
	[Export ("measuredPower", ArgumentSemantic.Strong)]
	NSNumber MeasuredPower { get; set; }

	// @property (nonatomic, strong) NSString * namespaceID;
	[Export ("namespaceID", ArgumentSemantic.Strong)]
	string NamespaceID { get; set; }

	// @property (nonatomic, strong) NSString * instanceID;
	[Export ("instanceID", ArgumentSemantic.Strong)]
	string InstanceID { get; set; }

	// @property (nonatomic, strong) NSString * url;
	[Export ("url", ArgumentSemantic.Strong)]
	string Url { get; set; }

	// @property (nonatomic, strong) ESTEddystoneTelemetry * telemetry;
	[Export ("telemetry", ArgumentSemantic.Strong)]
	ESTEddystoneTelemetry Telemetry { get; set; }

	// -(void)updateWithEddystone:(ESTEddystone *)eddystone;
	[Export ("updateWithEddystone:")]
	void UpdateWithEddystone (ESTEddystone eddystone);
}

// @interface ESTEddystoneUID : NSObject
[BaseType (typeof(NSObject))]
interface ESTEddystoneUID
{
	// @property (readonly, nonatomic, strong) NSString * namespaceID;
	[Export ("namespaceID", ArgumentSemantic.Strong)]
	string NamespaceID { get; }

	// @property (readonly, nonatomic, strong) NSString * instanceID;
	[Export ("instanceID", ArgumentSemantic.Strong)]
	string InstanceID { get; }

	// -(instancetype)initWithNamespaceID:(NSString *)namespaceID;
	[Export ("initWithNamespaceID:")]
	IntPtr Constructor (string namespaceID);

	// -(instancetype)initWithNamespaceID:(NSString *)namespaceID instanceID:(NSString *)instanceID;
	[Export ("initWithNamespaceID:instanceID:")]
	IntPtr Constructor (string namespaceID, string instanceID);
}

// @interface ESTEddystoneFilter : NSObject
[BaseType (typeof(NSObject))]
interface ESTEddystoneFilter
{
	// -(NSArray *)filterEddystones:(NSArray *)eddystones;
	[Export ("filterEddystones:")]
	[Verify (StronglyTypedNSArray), Verify (StronglyTypedNSArray)]
	NSObject[] FilterEddystones (NSObject[] eddystones);
}

// @protocol ESTEddystoneManagerDelegate <NSObject>
[Protocol, Model]
[BaseType (typeof(NSObject))]
interface ESTEddystoneManagerDelegate
{
	// @optional -(void)eddystoneManager:(ESTEddystoneManager *)manager didDiscoverEddystones:(NSArray *)eddystones withFilter:(ESTEddystoneFilter *)eddystoneFilter;
	[Export ("eddystoneManager:didDiscoverEddystones:withFilter:")]
	[Verify (StronglyTypedNSArray)]
	void EddystoneManager (ESTEddystoneManager manager, NSObject[] eddystones, ESTEddystoneFilter eddystoneFilter);

	// @optional -(void)eddystoneManagerDidFailDiscovery:(ESTEddystoneManager *)manager withError:(NSError *)error;
	[Export ("eddystoneManagerDidFailDiscovery:withError:")]
	void EddystoneManagerDidFailDiscovery (ESTEddystoneManager manager, NSError error);
}

// @interface ESTEddystoneManager : NSObject
[BaseType (typeof(NSObject))]
interface ESTEddystoneManager
{
	[Wrap ("WeakDelegate")]
	ESTEddystoneManagerDelegate Delegate { get; set; }

	// @property (nonatomic, weak) id<ESTEddystoneManagerDelegate> delegate;
	[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
	NSObject WeakDelegate { get; set; }

	// @property (readonly, nonatomic, strong) NSArray * filtersInDiscovery;
	[Export ("filtersInDiscovery", ArgumentSemantic.Strong)]
	[Verify (StronglyTypedNSArray)]
	NSObject[] FiltersInDiscovery { get; }

	// -(void)startEddystoneDiscoveryWithFilter:(ESTEddystoneFilter *)eddystoneFilter;
	[Export ("startEddystoneDiscoveryWithFilter:")]
	void StartEddystoneDiscoveryWithFilter (ESTEddystoneFilter eddystoneFilter);

	// -(void)stopEddystoneDiscoveryWithFilter:(ESTEddystoneFilter *)eddystoneFilter;
	[Export ("stopEddystoneDiscoveryWithFilter:")]
	void StopEddystoneDiscoveryWithFilter (ESTEddystoneFilter eddystoneFilter);
}

// @interface ESTEddystoneFilterUID : ESTEddystoneFilter
[BaseType (typeof(ESTEddystoneFilter))]
interface ESTEddystoneFilterUID
{
	// @property (readonly, nonatomic, strong) ESTEddystoneUID * eddystoneUID;
	[Export ("eddystoneUID", ArgumentSemantic.Strong)]
	ESTEddystoneUID EddystoneUID { get; }

	// -(instancetype)initWithUID:(ESTEddystoneUID *)eddystoneUID;
	[Export ("initWithUID:")]
	IntPtr Constructor (ESTEddystoneUID eddystoneUID);
}

// @interface ESTEddystoneFilterURL : ESTEddystoneFilter
[BaseType (typeof(ESTEddystoneFilter))]
interface ESTEddystoneFilterURL
{
	// @property (readonly, nonatomic, strong) NSString * eddystoneURL;
	[Export ("eddystoneURL", ArgumentSemantic.Strong)]
	string EddystoneURL { get; }

	// -(instancetype)initWithURL:(NSString *)eddystoneURL;
	[Export ("initWithURL:")]
	IntPtr Constructor (string eddystoneURL);
}

// @interface ESTEddystoneFilterURLDomain : ESTEddystoneFilter
[BaseType (typeof(ESTEddystoneFilter))]
interface ESTEddystoneFilterURLDomain
{
	// @property (readonly, nonatomic, strong) NSString * eddystoneURLDomain;
	[Export ("eddystoneURLDomain", ArgumentSemantic.Strong)]
	string EddystoneURLDomain { get; }

	// -(instancetype)initWithURLDomain:(NSString *)eddystoneURLDomain;
	[Export ("initWithURLDomain:")]
	IntPtr Constructor (string eddystoneURLDomain);
}
}