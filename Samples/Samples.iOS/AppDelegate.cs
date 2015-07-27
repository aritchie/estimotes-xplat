using System;
using Acr.UserDialogs;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.IO;


namespace Samples.iOS {

    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
			var path = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path;
			var dbPath = Path.Combine(path, "..", "Library/samples.db");

            Forms.Init();
            UserDialogs.Init();

			this.LoadApplication(new App(dbPath));
            return base.FinishedLaunching(app, options);
        }
    }
}
