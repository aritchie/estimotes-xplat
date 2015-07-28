using System;
using Acr;
using Estimotes;


namespace Samples.ViewModels {

	public class BeaconViewModel : ViewModel {

		public BeaconViewModel(IBeacon beacon) {
			this.Information = $"{beacon.Proximity}";
//			this.Information = $"{beacon.Proximity} {beacon.Identifier}";
			this.Details = $"Major: {beacon.Major} - Minor: {beacon.Minor} - ID: {beacon.Uuid}";
		}


		public string Information { get; }
		public string Details { get; }
	}
}