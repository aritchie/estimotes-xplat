using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Samples.ViewModels;


namespace Samples.Pages {

	public partial class EddystonePage : Acr.XamForms.ContentPage {

		public EddystonePage() {
            InitializeComponent();
            this.BindingContext = new EddystoneViewModel();
		}
	}
}