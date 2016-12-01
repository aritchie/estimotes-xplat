using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using EstimoteSdk;

namespace Samples.Droid {

    [Activity(Label = "Estimotes Xplat", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity {

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
			var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            Forms.Init(this, bundle);
            UserDialogs.Init(() => (Activity)Forms.Context);
            SystemRequirementsChecker.CheckWithDefaultDialogs(this);
            this.LoadApplication(new App(dbPath));
        }
    }
}