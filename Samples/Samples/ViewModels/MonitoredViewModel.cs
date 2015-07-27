using System;
using Acr;
using Samples.Models;


namespace Samples {
	
	public class MonitoredViewModel : ViewModel {
		
		public MonitoredViewModel(BeaconPing model) {
			this.Information = $"({model.IsEntering ? "Entering" : "Exited"}) {model.Uuid} - M: {model.Major} - m: {model.Minor}";
			this.Details = $"Bg: {model.IsAppInBackground} - {model.DateCreated:g}";
		}


		public string Information { get; private set; }
		public string Details { get; private set; }
	}
}
