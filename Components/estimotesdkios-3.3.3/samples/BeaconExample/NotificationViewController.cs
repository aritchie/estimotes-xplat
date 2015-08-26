using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Estimote;
using CoreLocation;

namespace BeaconExample
{
	partial class NotificationViewController : UIViewController
	{

		BeaconManager beaconManager;
		CLBeaconRegion beaconRegion;

		public CLBeacon Beacon { get; set; }
		public NotificationViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Notification Demo";
			beaconManager = new BeaconManager ();
			beaconRegion = new CLBeaconRegion (Beacon.ProximityUuid, ushort.Parse(Beacon.Major.ToString()), ushort.Parse(Beacon.Minor.ToString()), "BeaconSample");

			beaconRegion.NotifyOnEntry = enterSwitch.On;
			beaconRegion.NotifyOnExit = exitSwitch.On;

			enterSwitch.ValueChanged += HandleValueChanged;
			exitSwitch.ValueChanged += HandleValueChanged;

			beaconManager.StartMonitoringForRegion (beaconRegion);

		}

		void HandleValueChanged (object sender, EventArgs e)
		{
			beaconManager.StopMonitoringForRegion(beaconRegion);
			beaconRegion.NotifyOnEntry = enterSwitch.On;
			beaconRegion.NotifyOnExit = exitSwitch.On;
			beaconManager.StartMonitoringForRegion (beaconRegion);

		}
	}
}
