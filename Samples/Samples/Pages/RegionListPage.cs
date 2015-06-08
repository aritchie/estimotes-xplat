using System;
using Xamarin.Forms;
using Samples.ViewModels;


namespace Samples.Pages {

    public class RegionListPage : Acr.XamForms.ContentPage {

        public RegionListPage() {
            this.Title = "Regions";
            var listView = new Acr.XamForms.ListView {
                ItemTemplate = new DataTemplate(typeof(TextCell)) {
                    Bindings = {
                        { TextCell.TextProperty, new Binding("Identifier") },
                        { TextCell.DetailProperty, new Binding("UUID") }
                    }
                }
            };
            listView.SetBinding(Acr.XamForms.ListView.ItemsSourceProperty, "List");
            listView.SetBinding(Acr.XamForms.ListView.ItemClickCommandProperty, "Remove");

            var tb = new ToolbarItem {
                Text = "Add"
            };
            tb.SetBinding(ToolbarItem.CommandProperty, "Add");
            this.ToolbarItems.Add(tb);

            this.Content = listView;
			this.BindingContext = new RegionListViewModel();
        }
    }
}