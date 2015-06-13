using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Samples.ViewModels;


namespace Samples.Pages {
	
	public partial class RegionListPage : Acr.XamForms.ContentPage {
		
		public RegionListPage() {
			InitializeComponent();
			this.BindingContext = new RegionListViewModel();
		}
	}
}