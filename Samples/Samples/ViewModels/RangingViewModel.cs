using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Acr;
using Acr.UserDialogs;
using Estimotes;
using Samples.Pages;


namespace Samples.ViewModels {

    public class RangingViewModel : LifecycleViewModel {

		public override void OnStart() {
			this.List = new List<BeaconViewModel>();
			base.OnStart();
		}


        public override async void OnActivate() {
			base.OnActivate();
			this.List.Clear();
			this.OnPropertyChanged("List");

			EstimoteManager.Instance.Ranged += this.OnRanged;
            var status = await EstimoteManager.Instance.Initialize();
            if (status != BeaconInitStatus.Success)
				UserDialogs.Instance.Alert($"Beacon functionality failed - {status}");

            else {
                foreach (var region in App.Regions)
                    EstimoteManager.Instance.StartRanging(region);
            }
		}


		public override void OnDeactivate() {
			base.OnDeactivate();
            EstimoteManager.Instance.Ranged -= this.OnRanged;
			EstimoteManager.Instance.StopAllRanging();
		}


		void OnRanged(object sender, IEnumerable<IBeacon> beacons) {
			var list = new List<BeaconViewModel>();
			foreach (var beacon in beacons)
				list.Add(new BeaconViewModel(beacon));
			
            this.List = list;
			this.OnPropertyChanged("List");
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

        public IList<BeaconViewModel> List { get; private set; }
    }
}