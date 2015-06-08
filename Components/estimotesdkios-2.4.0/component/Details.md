This component binds the Estimote SDK for iOS so that it may be used in Xamarin.iOS projects to scan for iBeacons and Estimote Stickers.

The system requirements are iOS 7+ and Bluetooth Low Energy.

In your app’s AppDelegate on FinishedLaunching you can specify your Estimote app config:
```csharp
Config.SetupAppID (“appId”, “appToken”);
```

**On iOS 8**
You must specify `NSLocationAlwaysUsageDescription` or `NSLocationWhenInUseUsageDescription` in you info.plst file with a description that will be prompted to your users. Additionally, you must call the BeaconManager's `RequestAlwaysAuthorization` or `RequestWhenInUseAuthorization` methods. Please see sample for use.


The `BeaconManager` class is the primary means of interacting with Estimotes. Create an instance of this class, and use the `StartRandingBeacons` method, passing it `BeaconRegion`. You can then subscribe to the `RangedBeacons` event when a beacon is found.

The following code shows an example of how to use the `BeaconManager` to range for beacons.

```csharp
BeaconManager beaconManager;
BeaconRegion region;

public async override void ViewDidLoad ()
{
	base.ViewDidLoad ();
	this.Title = "Select Beacon";
	beaconManager = new BeaconManager ();
	beaconManager.ReturnAllRangedBeaconsAtOnce = true;
	var uuid = new NSUuid ("8492E75F-4FD6-469D-B132-043FE94921D8");
	region = new BeaconRegion (uuid, "BeaconSample");
	beaconManager.StartRangingBeacons(region);
	beaconManager.RangedBeacons += (sender, e) => 
	{
		new UIAlertView("Beacons Found", "Just found: " + e.Beacons.Length + " beacons.", null, "OK").Show();
	};
}
```

The Estimote SDK for iOS can also be used with Estimote Stickers. Here is an example of using the `NearableManager` to range for wearables.

**On iOS 8**
You must specify `NSLocationAlwaysUsageDescription` or `NSLocationWhenInUseUsageDescription` in you info.plst file with a description that will be prompted to your users. 

```csharp
NearableManager manager;
public override void ViewDidLoad ()
{
	base.ViewDidLoad ();

	TableView.WeakDataSource = this;
	TableView.WeakDelegate = this;

	manager = new NearableManager ();

	manager.RangedNearables += (sender, e) => 
	{
		new UIAlertView("Nearables Found", "Just found: " + e.Nearables.Length + " nearables.", null, "OK").Show();
	};

	manager.StartRanging (NearableType.All);
}

```