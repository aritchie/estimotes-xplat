using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Acr.Notifications;
using Acr.UserDialogs;
using Estimotes;
using Samples.Pages;
using Xamarin.Forms;


namespace Samples {

    public class App : Application {
        public static bool IsBackgrounded { get; private set; }
        public static IList<BeaconRegion> Regions { get; private set; }


        static App() {
            Regions = new List<BeaconRegion> {
			    new BeaconRegion("AE189F8B-9011-4859-B53E-C65314880E22", "ice"),
			    new BeaconRegion("AE189F8B-9011-4859-B53E-C65314880E22", "fire"),
			    new BeaconRegion("AE189F8B-9011-4859-B53E-C65314880E22", "mint"),
//				new BeaconRegion("AE189F8B-9011-4859-B53E-C65314880E22", "blueberry")
				new BeaconRegion("B9407F30-F5F8-466E-AFF9-25556B57FE6D", "blueberry")
            };
        }


        public App() {
            this.MainPage = new NavigationPage(new MainPage());
        }


       protected override void OnStart() {
            base.OnStart();
            App.IsBackgrounded = false;
            EstimoteManager.Instance.EnteredRegion += (sender, region) => Notify("Entered Region", "You are near {0}", region);
            EstimoteManager.Instance.ExitedRegion += (sender, region) => Notify("Exited Region", "You have moved out of range of {0}", region);

            EstimoteManager
                .Instance
                .Initialize()
                .ContinueWith(x => {
                    if (x.Result)
                        EstimoteManager.Instance.StartMonitoring(App.Regions.ToArray());
                });
        }


        protected override void OnResume() {
            base.OnResume();
            App.IsBackgrounded = false;
        }


        protected override void OnSleep() {
            base.OnSleep();
            App.IsBackgrounded = true;
        }


        public static void Notify(string title, string msgFormat, BeaconRegion region) {
            var msg = String.Format(msgFormat, region.Identifier);

            try {
                if (App.IsBackgrounded)
                    Notifications.Instance.Send(title, msg);
                else
                    UserDialogs.Instance.Alert(title, msg);
            }
            catch (Exception ex) {
                Debug.WriteLine("Notification Error: " + ex);
            }
        }
    }
}
