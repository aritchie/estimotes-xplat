using System;
using Acr;
using Samples.Models;


namespace Samples.ViewModels {

	public class MonitoredViewModel : ViewModel {

		public MonitoredViewModel(BeaconPing model) {

			this.Information = model.IsEntering ? "Entered " : "Exited ";
            this.Information += $"{model.Uuid} - M: {model.Major} - m: {model.Minor}";
			this.Details = $"Bg: {model.IsAppInBackground} - {model.DateCreated:g}";
		}


		public string Information { get; }
		public string Details { get; }
	}
}
