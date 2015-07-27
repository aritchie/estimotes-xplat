using System;
using Acr;
using Estimotes;


namespace Samples.ViewModels {

	public class BeaconViewModel : ViewModel {

		public BeaconViewModel(IBeacon beacon) {
			this.Information = $"{beacon.Proximity} {beacon.Uuid}";
			this.Details = $"Major: {beacon.Major} - Minor: {beacon.Minor}";
		}


		public string Information { get; }
		public string Details { get; }
	}
}