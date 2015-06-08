using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using Acr;
using Acr.UserDialogs;
using Estimotes;


namespace Samples.ViewModels {

	public class RegionListViewModel : LifecycleViewModel {

	    public override void OnActivate() {
			base.OnActivate();
            this.List = App.Regions.ToList();
	    }


		private IEnumerable<BeaconRegion> list;
		public IEnumerable<BeaconRegion> List {
			get { return this.list; }
			private set {
				this.list = value;
				this.OnPropertyChanged();
			}
		}


		private ICommand addCmd;
		public ICommand Add {
			get {
				this.addCmd = this.addCmd ?? new Acr.Command(async () => await this.OnAddRegion());
				return this.addCmd;
			}
		}


		private Acr.Command<BeaconRegion> removeCmd;
		public Acr.Command<BeaconRegion> Remove {
			get {
				this.removeCmd = this.removeCmd ?? new Acr.Command<BeaconRegion>(async x => await this.OnRemoveRegion(x));
				return this.removeCmd;
			}
		}


		private async Task OnRemoveRegion(BeaconRegion region) {
            var confirm = await UserDialogs.Instance.ConfirmAsync(String.Format("Stop Monitoring Region {0}?", region.Identifier));
            if (!confirm)
                return;

            App.Regions.Remove(region);
            EstimoteManager.Instance.StopMonitoring(region);
            this.List = App.Regions.ToList();
		}


		private async Task OnAddRegion() {
			var cfg = new PromptConfig()
				.SetText("B9407F30-F5F8-466E-AFF9-25556B57FE6D")
				.SetMessage("Enter the region UUID");

			var uuid = await UserDialogs.Instance.PromptAsync(cfg);
			if (!uuid.Ok)
				return;

			var id = await UserDialogs.Instance.PromptAsync("Enter the beacon identifier");
			if (!id.Ok)
				return;

			try {
				var region = new BeaconRegion(uuid.Text.Trim(), id.Text.Trim());
                EstimoteManager.Instance.StartMonitoring(region);
                App.Regions.Add(region);
				this.List = App.Regions.ToList();
			}
			catch (Exception ex) {
				UserDialogs.Instance.Alert(ex.ToString());
			}
		}
	}
}