using System;
using System.Collections.Generic;
using System.Diagnostics;
using Acr;
using Acr.Notifications;
using Estimotes;
using Samples.Pages;
using Xamarin.Forms;
using Samples.Models;


namespace Samples {

    public class App : Application {
		public static SampleDbConnection Data { get; private set; }

/*
With only UUID: it consists of all beacons with a given UUID. For example: a region defined with default Estimote UUID would consist of all Estimote Beacons with unchanged UUID.
With UUID and Major: it consists of all beacons using a specific combination of UUID and Major. For example: all Estimote Beacons with default UUID and Major set to 1.
With UUID, Major and Minor: it consists of only a single beacon (keep in mind that Estimote Cloud prevents having two beacons with the same ID’s). For example, one with default Estimote UUID, Major set to 1 and Minor set to 1.
		*/
        public static bool IsBackgrounded { get; private set; }
        public static IList<BeaconRegion> Regions { get; } = new List<BeaconRegion> {
//			new BeaconRegion("doubleregion", "B9407F30-F5F8-466E-AFF9-25556B57FE6D", 46876)
			new BeaconRegion("blueberry", "B9407F30-F5F8-466E-AFF9-25556B57FE6D", 46876, 60214),
			new BeaconRegion("mint", "B9407F30-F5F8-466E-AFF9-25556B57FE6D", 47263, 31286)
        };


		public App(string databasePath) {
			Data = new SampleDbConnection(databasePath);
			this.MainPage = new TabbedPage {
				Children = {
					new NavigationPage(new RangingPage { Title = "Estimotes - Ranging" }) { Title = "Ranging" },
					new NavigationPage(new MonitorPage { Title = "Estimotes - Monitoring" }) { Title = "Monitoring" }
				}
			};
        }


        protected override async void OnStart() {
            base.OnStart();
			Notifications.Instance.Badge = 0; // just waking up for permissions
            App.IsBackgrounded = false;

            var ei = EstimoteManager.Instance;
			ei.RegionStatusChanged += OnBeaconRegionStatusChanged;
            var result = await EstimoteManager.Instance.Initialize();
            if (result != BeaconInitStatus.Success)
                return;

            ei.StopAllMonitoring();
			foreach (var region in Regions)
				ei.StartMonitoring(region);
        }


        protected override void OnResume() {
            base.OnResume();
            App.IsBackgrounded = false;
        }


        protected override void OnSleep() {
            base.OnSleep();
            App.IsBackgrounded = true;
			EstimoteManager.Instance.StopAllRanging();
        }


		static void OnBeaconRegionStatusChanged(object sender, BeaconRegionStatusChangedEventArgs args) {
			App.Data.Insert(new BeaconPing {
				Identifier = args.Region.Identifier,
				Uuid = args.Region.Uuid,
				Major = args.Region.Major.Value,
				Minor = args.Region.Minor.Value,
				DateCreated = DateTime.Now,
				IsEntering = args.IsEntering,
				IsAppInBackground = App.IsBackgrounded
			});

			if (args.IsEntering)
				Notifications.Instance.Send("Entered Region", $"You have entered {args.Region.Identifier}");
			else
				Notifications.Instance.Send("Exited Region", $"You have exited {args.Region.Identifier}");
		}
    }
}
