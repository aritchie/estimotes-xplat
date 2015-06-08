using System;
using System.Drawing;

using Foundation;
using UIKit;
using Estimote;
using System.Text;

namespace NearableMonitoringExample
{
	public partial class NearableMonitoringExampleViewController : UIViewController
	{
		public NearableMonitoringExampleViewController (IntPtr handle) : base (handle)
		{
		}

		NearableManager nearableManager;
		public Nearable Nearable { get; set;}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.Title = "Nearable: " + Nearable.Identifier;
			nearableManager = new NearableManager ();

			nearableManager.RangedNearable += (sender, e) => {
				Nearable = e.Nearable;
				UpdateStats ();
			};

			nearableManager.EnteredIdentifierRegion += (sender, e) => 
			{
				if (enterSwitch.On) {
					LabelNotification.Text = "Enter region: " + e.Identifier;

					var notification = new UILocalNotification ();
					notification.AlertBody = LabelNotification.Text;

					UIApplication.SharedApplication.PresentLocalNotificationNow (notification);
				}
			};

			nearableManager.ExitedIdentifierRegion += (sender, e) => 
			{
				if (exitSwitch.On) {
					LabelNotification.Text = "Exit region: " + e.Identifier;
					var notification = new UILocalNotification ();
					notification.AlertBody = LabelNotification.Text;

					UIApplication.SharedApplication.PresentLocalNotificationNow (notification);
				}
			};
		}

		private void UpdateStats()
		{
			var stringBuilder = new StringBuilder ();
			stringBuilder.AppendLine ("Nearable Info:");
			stringBuilder.AppendLine ("Power: " + Nearable.Power.ToString());
			stringBuilder.AppendLine ("Motion State Duration: " + Nearable.CurrentMotionStateDuration.ToString());
			stringBuilder.AppendLine ("Firmware State: " + Nearable.FirmwareState.ToString());
			stringBuilder.AppendLine ("Firmware Version: " + Nearable.FirmwareVersion.ToString());
			stringBuilder.AppendLine ("Hardware Version: " + Nearable.HardwareVersion.ToString());
			stringBuilder.AppendLine ("Idle Battery Voltage: " + Nearable.IdleBatteryVoltage.ToString());
			stringBuilder.AppendLine ("Is Moving: " + Nearable.IsMoving.ToString());
			stringBuilder.AppendLine ("Orientation: " + Nearable.Orientation.ToString());
			stringBuilder.AppendLine ("Previous Motion State Duration: " + Nearable.PreviousMotionStateDuration.ToString());
			stringBuilder.AppendLine ("Rssi: " + Nearable.Rssi.ToString());
			stringBuilder.AppendLine ("Stress Battery Voltage: " + Nearable.StressBatteryVoltage.ToString());
			stringBuilder.AppendLine ("Temperature: " + Nearable.Temperature.ToString());
			stringBuilder.AppendLine ("XAcceleration: " + Nearable.XAcceleration.ToString());
			stringBuilder.AppendLine ("YAcceleration: " + Nearable.YAcceleration.ToString());
			TextViewInfo.Text = stringBuilder.ToString ();
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			nearableManager.StopMonitoring (Nearable.Identifier);
			nearableManager.StopRanging (Nearable.Identifier);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			UpdateStats ();

			nearableManager.StartMonitoring (Nearable.Identifier);
			nearableManager.StartRanging (Nearable.Identifier);

			switch (Nearable.Color) {
			case NearableColor.BlueberryPie:
				BackgroundView.BackgroundColor = UIColor.Blue;
				break;
			case NearableColor.CandyFloss:
				BackgroundView.BackgroundColor = UIColor.Purple;
				break;
			case NearableColor.IcyMarshmallow:
				BackgroundView.BackgroundColor = UIColor.LightGray;
				break;
			case NearableColor.LemonTart:
				BackgroundView.BackgroundColor = UIColor.Yellow;
				break;
			case NearableColor.MintCocktail:
				BackgroundView.BackgroundColor = UIColor.Green;
				break;
			case NearableColor.SweetBeetroot:
				BackgroundView.BackgroundColor = UIColor.Cyan;
				break;
			default:
				BackgroundView.BackgroundColor = UIColor.Gray;
				break;
			}
		}
		#endregion

	}
}

