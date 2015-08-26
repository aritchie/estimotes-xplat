using System;
using Acr;
using Samples.Models;


namespace Samples.ViewModels {

	public class MonitoredViewModel : ViewModel {

		public MonitoredViewModel(BeaconPing model) {
			this.Information = $"{model.Type}  {model.Identifier}";
			this.Details = $"M: {model.Major} - m: {model.Minor} - {model.DateCreated:g}";
		}


		public string Information { get; }
		public string Details { get; }
	}
}
