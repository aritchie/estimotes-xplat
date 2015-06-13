using System;
using Acr.UserDialogs;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Estimote;


namespace Samples.iOS {

    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            Forms.Init();
            UserDialogs.Init();

            this.LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }
    }
}
