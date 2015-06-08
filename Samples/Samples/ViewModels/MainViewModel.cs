using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Acr;
using Acr.UserDialogs;
using Estimotes;


namespace Samples.ViewModels {

    public class MainViewModel : LifecycleViewModel {

        public override async void OnStart() {
            base.OnStart();
            this.IsBeaconFunctionalityAvailable = await EstimoteManager.Instance.IsAvailable();
            if (!this.IsBeaconFunctionalityAvailable)
				UserDialogs.Instance.Alert("Beacon functionality not enabled in app permissions");
        }


        public override void OnActivate() {
			base.OnActivate();
            this.List = new List<Beacon>();
            if (this.IsBeaconFunctionalityAvailable) {
                EstimoteManager.Instance.Ranged += this.OnRanged;
                EstimoteManager.Instance.StartRanging(App.Regions.ToArray());
            }
		}


		public override void OnDeactivate() {
			base.OnDeactivate();
            if (this.IsBeaconFunctionalityAvailable) {
                EstimoteManager.Instance.Ranged -= this.OnRanged;
                EstimoteManager.Instance.StopRanging(App.Regions.ToArray());
            }
		}


		private void OnRanged(object sender, IEnumerable<Beacon> beacons) {
            this.List = beacons.ToList();
            //if (beacon.Proximity == Proximity.Unknown) {
            //    Debug.WriteLine("Removing " + beacon.Region.Identifier);
            //    this.List.Remove(beacon);
            //    this.OnPropertyChanged("List");
            //}
            //else {
            //    var index = this.List.IndexOf(beacon);
            //    if (index < 0)
            //        Debug.WriteLine("Adding " + beacon.Region.Identifier);
            //    else {
            //        Debug.WriteLine("Updating " + beacon.Region.Identifier);
            //        this.List.RemoveAt(index);
            //    }
            //    this.List.Add(beacon);
            //    this.OnPropertyChanged("List");
            //}
		}


        public IList<Beacon> List { get; private set; }

        private bool isBeaconFunctionalityAvailable;
        public bool IsBeaconFunctionalityAvailable {
            get { return this.isBeaconFunctionalityAvailable; }
            private set { this.SetProperty(ref this.isBeaconFunctionalityAvailable, value); }
        }
    }
}