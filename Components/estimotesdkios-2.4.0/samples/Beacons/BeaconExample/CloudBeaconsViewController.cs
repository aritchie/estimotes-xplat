using System;
using Estimote;
using UIKit;
using Foundation;

namespace BeaconExample
{
	public class CloudBeaconsViewController : UITableViewController
	{
		BeaconManager beaconManager;
		BeaconRegion region;
		BeaconVO[] beacons;
		public CloudBeaconsViewController ()
		{
		}
		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Cloud Beacons";
			beaconManager = new BeaconManager ();
		
			try
			{
				beacons = await beaconManager.FetchEstimoteBeaconsAsync ();
				TableView.ReloadData();
			}
			catch(Exception ex) {
				new UIAlertView ("Error", "Unable to fetch cloud beacons, ensure you have set Config in AppDelegate", null, "OK").Show ();
			}
		}


	

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			if (beacons == null)
				return 0;
			else
				return beacons.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("cellID");
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, "cellId");
			}

			var beacon = beacons [indexPath.Row];

			cell.TextLabel.Text = string.Format ("major: {0}, Minor {1}", beacon.Major, beacon.Minor);
			cell.DetailTextLabel.Text = "Color: " + beacon.Color.ToString () + " Name: " + beacon.Name ;

			return cell;
		}
	}
}

