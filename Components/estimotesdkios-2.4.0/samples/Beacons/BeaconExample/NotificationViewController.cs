using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Estimote;

namespace BeaconExample
{
	partial class NotificationViewController : UIViewController
	{

		BeaconManager beaconManager;
		BeaconRegion beaconRegion;

		public Beacon Beacon { get; set; }
		public NotificationViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Notification Demo";
			beaconManager = new BeaconManager ();
			beaconRegion = new BeaconRegion (Beacon.ProximityUUID, Beacon.Major, Beacon.Minor, "BeaconSample");

			beaconRegion.NotifyOnEntry = enterSwitch.On;
			beaconRegion.NotifyOnExit = exitSwitch.On;

			enterSwitch.ValueChanged += HandleValueChanged;
			exitSwitch.ValueChanged += HandleValueChanged;

			beaconManager.StartMonitoring (beaconRegion);


			beaconManager.ExitedRegion += (sender, e) => 
			{
				var notification = new UILocalNotification();
				notification.AlertBody = "Exit region notification";
				UIApplication.SharedApplication.PresentLocalNotificationNow(notification);
			};

			beaconManager.EnteredRegion += (sender, e) => 
			{
				var notification = new UILocalNotification();
				notification.AlertBody = "Enter region notification";
				UIApplication.SharedApplication.PresentLocalNotificationNow(notification);
			};
		}

		void HandleValueChanged (object sender, EventArgs e)
		{
			beaconManager.StopMonitoring(beaconRegion);
			beaconRegion.NotifyOnEntry = enterSwitch.On;
			beaconRegion.NotifyOnExit = exitSwitch.On;
			beaconManager.StartMonitoring (beaconRegion);

		}
	}
}
