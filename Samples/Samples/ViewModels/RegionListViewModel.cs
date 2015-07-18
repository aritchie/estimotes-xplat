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

        public RegionListViewModel() {
            this.Remove = new Acr.Command<BeaconRegion>(async x => await this.OnRemoveRegion(x));
            this.Add = new Acr.Command(async () => await this.OnAddRegion());
        }


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


		public ICommand Add { get; }
		public Command<BeaconRegion> Remove { get; }


		private async Task OnRemoveRegion(BeaconRegion region) {
            var confirm = await UserDialogs.Instance.ConfirmAsync($"Stop Monitoring Region {region.Uuid}?");
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

			var region = new BeaconRegion("com.acrapps", uuid.Text);
			try {
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