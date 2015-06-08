This component binds the Estimote SDK for Android so that it may be used in Xamarin.Android projects.

The system requirements are Android 4.3 and Bluetooth Low Energy.

The `BeaconManager` class is the primary means of interating with Estimotes. Create an instance of this class, and use the `.Connect` method, passing it `BeaconManager.IServiceReadyCallback` object. When the BeaconManager is up and running, it will notify clients by call `BeaconManager.IServiceReadyCallback.OnServiceReady()`. At this point the client can start ranging or monitoring for the Estimotes.

The following code shows an example of how to use the `BeaconManager`.

```csharp

using EstimoteSdk;
namespace Estimotes.Droid
{
    [Activity(Label = "NotifyDemoActivity")]	
    public class NotifyDemoActivity : Activity, BeaconManager.IServiceReadyCallback
    {
        static readonly int NOTIFICATION_ID = 123321;

        BeaconManager _beaconManager;
        Region _region;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.notify_demo);

            _region = this.GetBeaconAndRegion().Item2;
            _beaconManager = new BeaconManager(this);

            // Default values are 5s of scanning and 25s of waiting time to save CPU cycles.
            // In order for this demo to be more responsive and immediate we lower down those values.
            _beaconManager.SetBackgroundScanPeriod(TimeUnit.Seconds.ToMillis(1), 0);
            _beaconManager.EnteredRegion += (sender, e) => {
                // Do something as the device has entered in region for the Estimote.
            };
            _beaconManager.ExitedRegion += (sender, e) => {
                // Do something as the device has left the region for the Estimote.            
            };
        
        }

        protected override void OnResume()
        {
            base.OnResume();
            _beaconManager.Connect(this);
        }

        public void OnServiceReady()
        {
            // This method is called when BeaconManager is up and running.
            _beaconManager.StartMonitoring(_region);
        }

        protected override void OnDestroy()
        {
        	// Make sure we disconnect from the Estimote.
            _beaconManager.Disconnect();
            base.OnDestroy();
        }
    }
}

```
