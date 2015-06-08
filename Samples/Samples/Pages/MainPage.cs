using System;
using Xamarin.Forms;
using Samples.ViewModels;


namespace Samples.Pages {

    public class MainPage : Acr.XamForms.ContentPage {

        public MainPage() {
			this.Title = "iBeacons";

			this.ToolbarItems.Add(new ToolbarItem {
				Text = "Regions",
				Command = new Command(async () => {
                    var vm = (MainViewModel)this.BindingContext;
                    if (vm.IsBeaconFunctionalityAvailable)
				        await this.Navigation.PushAsync(new RegionListPage());
				})
			});

			var listView = new ListView {
				ItemTemplate = new DataTemplate(typeof(TextCell)) {
					Bindings = {
						{ TextCell.TextProperty, new Binding("Identifier") },
						{ TextCell.DetailProperty, new Binding("Proximity") }
					}
				}
			};
            listView.SetBinding(ListView.ItemsSourceProperty, "List");
            this.Content = listView;

			this.BindingContext = new MainViewModel();
        }
    }
}
