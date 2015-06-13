using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Samples.ViewModels;


namespace Samples.Pages {

	public partial class MainPage : Acr.XamForms.ContentPage {
	
		public MainPage() {
			InitializeComponent();
			this.BindingContext = new MainViewModel();
		}
	}
}

