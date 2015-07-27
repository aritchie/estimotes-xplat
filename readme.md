#ESTIMOTES PLUGIN FOR XAMARIN

---

This is a cross platform implementation of the estimote library for beacons.  It will allow you to make beacon calls for your PCL library.
Please be sure to read through the samples found in this repository!


##iOS Setup

Make sure to install the nuget package in your platform project.
In your Info.plist, add the following key:

    <key>NSLocationAlwaysUsageDescription</key>
    <string>ENTER YOUR REASON FOR LOCATION TRACKING</string>


##Android Specific Setup

Make sure to install the nuget package in your platform project.  Android 4.3+ is required!

You need to add BLUETOOTH and BLUETOOTH_ADMIN privileges to your android project


##To Use

First thing to do is initialize the plugin

    var available = await EstimotesManager.Instance.Initialize();
    if (!available) {
        ... You have a problem with permissions or bluetooth is unavailable on the device
    }

Now hook up to the event(s) you want to use

    EstimotesManager.Instance.Ranged += (sender, beacons) => {};
    EstimotesManager.Instance.EnteredRegion += (sender, region) => {};
    EstimotesManager.Instance.ExitedRegion += (sender, region) => {};

And to actually start or stop the process (optionally, you can also pass major/minor combinations)

    EstimotesManager.Instance.StartRanging(new BeaconRegion("Beacon Identifier", "Your UUID"));
    EstimotesManager.Instance.StopRanging(new BeaconRegion("Beacon Identifier", "Your UUID"));

    EstimotesManager.Instance.StartMonitoring(new BeaconRegion("Beacon Identifier", "Your UUID"));
    EstimotesManager.Instance.StopMonitoring(new BeaconRegion("Beacon Identifier", "Your UUID"));
