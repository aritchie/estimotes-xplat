using System;
using System.Collections.Generic;
using System.Diagnostics;
using Acr;
using Acr.Notifications;
using Acr.UserDialogs;
using Estimotes;
using Samples.Pages;
using Xamarin.Forms;


namespace Samples {

    public class App : Application {

        public static bool IsBackgrounded { get; private set; }
        public static IList<BeaconRegion> Regions { get; } = new List<BeaconRegion> {
			new BeaconRegion("com.acrapps", "AE189F8B-9011-4859-B53E-C65314880E22"),
			new BeaconRegion("com.acrapps", "B9407F30-F5F8-466E-AFF9-25556B57FE6D")
        };


        public App() {
            this.MainPage = new NavigationPage(new MainPage());
        }


       protected override async void OnStart() {
            base.OnStart();
            App.IsBackgrounded = false;

            var ei = EstimoteManager.Instance;
            ei.EnteredRegion += (sender, region) => Notify("Entered Region", "You have entered a region");
            ei.ExitedRegion += (sender, region) => Notify("Exited Region", "You have exited a region");

            var result = await EstimoteManager.Instance.Initialize();
            if (!result)
                return;

            Regions.Each(x => ei.StartMonitoring(x.Uuid));
        }


        protected override void OnResume() {
            base.OnResume();
            App.IsBackgrounded = false;
        }


        protected override void OnSleep() {
            base.OnSleep();
            App.IsBackgrounded = true;
        }


        static void Notify(string title, string msg) {
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
