using System;
using Samples.ViewModels;


namespace Samples.Pages {

	public partial class MonitorPage : Acr.XamForms.ContentPage {

		public MonitorPage() {
            InitializeComponent();
            this.BindingContext = new MonitorViewModel();
		}
	}
}

