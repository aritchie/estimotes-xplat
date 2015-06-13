#ESTIMOTES PLUGIN FOR XAMARIN

---

This is a cross platform implementation of the estimote library for beacons.  It will allow you to make beacon calls for your PCL library.
Please be sure to read through the samples found in this repository!


##iOS Setup

Make sure to install the nuget package in your platform project.
In your project properties, 


##Android Specific Setup

Make sure to install the nuget package in your platform project.

In your AndroidManifest.xml, make to add the estimote service and necessary permissions (example below):

	<application android:label="Your App" android:icon="@drawable/icon">
		<service android:name="com.estimote.sdk.service.BeaconService" android:exported="false" />
	</application>
	<uses-permission android:name="android.permission.BLUETOOTH" />
	<uses-permission android:name="android.permission.BLUETOOTH_ADMIN" />