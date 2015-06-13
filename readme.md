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

In your AndroidManifest.xml, make to add the estimote service and necessary permissions (example below):

    <application android:label="Your App" android:icon="@drawable/icon">
        <service android:name="com.estimote.sdk.service.BeaconService" android:exported="false" />
    </application>
    <uses-permission android:name="android.permission.BLUETOOTH" />
    <uses-permission android:name="android.permission.BLUETOOTH_ADMIN" />


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

And to actually start or stop the process

    EstimotesManager.Instance.StartRanging(new BeaconRegion("Your UUID", "Beacon Identifier"));
    EstimotesManager.Instance.StopRanging(new BeaconRegion("Your UUID", "Beacon Identifier"));

    EstimotesManager.Instance.StartMonitoring(new BeaconRegion("Your UUID", "Beacon Identifier"));
    EstimotesManager.Instance.StopMonitoring(new BeaconRegion("Your UUID", "Beacon Identifier"));
