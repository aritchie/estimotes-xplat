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

        public static bool IsBackgrounded { get; private set; }
        public static IList<BeaconRegion> Regions { get; } = new List<BeaconRegion> {
			new BeaconRegion("com.acrapps", "AE189F8B-9011-4859-B53E-C65314880E22"),
			new BeaconRegion("default",  "B9407F30-F5F8-466E-AFF9-25556B57FE6D")
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
				if (!ei.StartMonitoring(region))
					throw new ArgumentException("Just cancelled all regions - this should go");
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
				Notify("Entered Region", "You have entered a region");
			else
				Notify("Exited Region", "You have exited a region");
		}


        static void Notify(string title, string msg) {
			Notifications.Instance.Send(title, msg);
        }
    }
}
