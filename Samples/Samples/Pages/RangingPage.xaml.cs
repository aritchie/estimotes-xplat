using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Samples.ViewModels;


namespace Samples.Pages {

	public partial class RangingPage : Acr.XamForms.ContentPage {
	
		public RangingPage() {
			InitializeComponent();
			this.BindingContext = new RangingViewModel();
		}
	}
}

