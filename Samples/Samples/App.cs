using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            new BeaconRegion("whites", "B9407F30-F5F8-466E-AFF9-25556B57FE6D")
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


		static async void OnBeaconRegionStatusChanged(object sender, BeaconRegionStatusChangedEventArgs args) {
			App.Data.Insert(new BeaconPing {
				Identifier = args.Region.Identifier,
				Uuid = args.Region.Uuid,
				Major = args.Region.Major ?? 0,
				Minor = args.Region.Minor ?? 0,
				DateCreated = DateTime.Now,
				Type = args.IsEntering ? BeaconPingType.MonitorEntering : BeaconPingType.MonitorExiting
			});

			if (!args.IsEntering)
				Notifications.Instance.Send("Exited Region", $"You have exited {args.Region.Identifier}");
			else {
                try {
                    var beacons = await EstimoteManager.Instance.FetchNearbyBeacons(args.Region);
                    foreach (var beacon in beacons) {
			            App.Data.Insert(new BeaconPing {
				            Identifier = args.Region.Identifier,
				            Uuid = beacon.Uuid,
				            Major = beacon.Major,
				            Minor = beacon.Minor,
				            DateCreated = DateTime.Now,
				            Type = App.IsBackgrounded ? BeaconPingType.RangedBackground : BeaconPingType.RangedForeground
			            });
                    }
                    Notifications.Instance.Send("Entered Region", $"You have entered {args.Region.Identifier}");
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex);
                }
            }
		}
    }
}
