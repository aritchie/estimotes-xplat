using System;
using UIKit;
using Estimote;
using CoreGraphics;
using Foundation;

namespace BeaconExample
{
	public class DistanceDemoViewController : UIViewController
	{
		Beacon beacon;
		BeaconManager beaconManager;
		BeaconRegion beaconRegion;
		UIImageView backgroundImage, positionDot;

		int MaxDistance = 20;
		int TopMargin = 150;

		public DistanceDemoViewController (Beacon beacon)
		{
			this.beacon = beacon;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Distance Demo";
			backgroundImage = new UIImageView(UIImage.FromFile ("distance_bkg"));
			backgroundImage.Frame = UIScreen.MainScreen.Bounds;
			backgroundImage.ContentMode = UIViewContentMode.ScaleToFill;

			View.AddSubview (backgroundImage);
			View.BackgroundColor = UIColor.White;

			//var beaconImageView = new UIImageView (UIImage.FromFile ("beacon_linearnie"));
			//beaconImageView.Center = new CoreGraphics.CGPoint (View.Center.X, 100);
			//View.AddSubview (beaconImageView);


			positionDot = new UIImageView (UIImage.FromFile ("dot_image"));
			positionDot.Center = View.Center;
			View.AddSubview (positionDot);

			//Beacon manager setup.
			beaconManager = new BeaconManager ();
			beaconRegion = new BeaconRegion (beacon.ProximityUUID, beacon.Major, beacon.Minor, "BeaconSample");
			beaconManager.StartRangingBeacons(beaconRegion);
		
			beaconManager.RangedBeacons += (sender, e) => 
			{

				if(e.Beacons.Length == 0)
					return;

				var first = e.Beacons[0];
				UpdateDotPosition(first.Distance.FloatValue);
			};
		}

		private void UpdateDotPosition(float distance)
		{
			var step = (View.Frame.Size.Height - TopMargin) / MaxDistance;

			var newY = TopMargin + (distance * step);

			positionDot.Center = new CGPoint (positionDot.Center.X, newY);
		}

		public override void ViewDidDisappear (bool animated)
		{

			beaconManager.StopRangingBeacons (beaconRegion);
			base.ViewDidDisappear (animated);

		}


	}
}

