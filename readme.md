# ACR ESTIMOTES PLUGIN FOR XAMARIN

---

This is a cross platform implementation of the estimote library for beacons.  It will allow you to make beacon calls for your PCL library.
Please be sure to read through the samples found in this repository!

This library currently only supported on iOS and Android.  You can get it on nuget at [https://www.nuget.org/packages/Estimotes.Xplat/](https://www.nuget.org/packages/Estimotes.Xplat/)


## SETUP
### iOS

Make sure to install the nuget package in your platform project.
In your Info.plist, add the following key:

    <!-- needed for background monitoring or foreground ranging-->
    <key>NSLocationAlwaysUsageDescription</key>
    <string>ENTER YOUR REASON FOR LOCATION TRACKING</string>

    <!-- if you don't need background monitoring, use this -->
    <key>NSLocationWhenInUseDescription</key>
    <string>ENTER YOUR REASON FOR LOCATION TRACKING</string>

### Android

Make sure to install the nuget package in your platform project.  Android 4.3+ is required!

You need to add BLUETOOTH and BLUETOOTH_ADMIN privileges to your android project


## USAGE

First thing to do is initialize the plugin

    var status = await EstimoteManager.Instance.Initialize(); // optionally pass false to authorize foreground ranging only
    if (status != BeaconInitStatus.Success) {
        ... You have a problem with permissions or bluetooth is unavailable on the device, use the enum to figure out what!
    }

Now hook up to the event(s) you want to use

    EstimoteManager.Instance.Ranged += (sender, beacons) => {};
    EstimoteManager.Instance.RegionStatusChanged += (sender, region) => {};

And to actually start or stop the process (optionally, you can also pass major/minor combinations)

    // for exact distancing, use ranging - requires your app to be in the foreground
    EstimoteManager.Instance.StartRanging(new BeaconRegion("Beacon Identifier", "Your UUID"));
    EstimoteManager.Instance.StopRanging(new BeaconRegion("Beacon Identifier", "Your UUID"));

    // for background monitoring when you don't care about actual distance
    EstimoteManager.Instance.StartMonitoring(new BeaconRegion("Beacon Identifier", "Your UUID"));
    EstimoteManager.Instance.StopMonitoring(new BeaconRegion("Beacon Identifier", "Your UUID"));


## FAQ

1. What is the difference between ranging and monitoring?
* Please read the estimote tutorials to learn all about how beacons works - [http://developer.estimote.com/](http://developer.estimote.com/)

2. Does this library support other brands of manufacturuers?
* I won't personally support anything beyond estimote beacons for this project because it is driven by estimote SDK's which are coded to their beacons.  If you have success with other beacon brands - great.

3. I'm monitoring by UUID, but the major and minor values do not come back for a beacon
* This is how monitoring works.  It echos what you are monitoring.  If you want to get more info for the beacon(s) that triggered the monitor entry, you have to range to get additional details.
  This is the purpose of FetchNearbyBeacons(BeaconRegion).  WARNING: On iOS, you only get about 5 seconds after a monitor event to do something.  Range and save, broadcasting to the server isn't always going to work if their is latency.

4. How many regions can I monitor/range at one time?
* On android, I don't believe their is a limitation.  On iOS, the maximum is 20.  This library does not protect against errors around this maximum

5. Estimote.iOS.Binding doesn't compile
* Don't worry about it.  It is a test binding at the moment and isn't actively used.