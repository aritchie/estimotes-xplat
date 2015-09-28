using System;
using Estimotes;


namespace Samples.ViewModels {

    public class EddystoneViewModel {

        public string Identifier { get; }
		public string Proximity { get; }
		public string MacAddress { get; }
		public double? Temperature { get; }


        public EddystoneViewModel(IEddystone eddystone) {
			this.Proximity = eddystone.Proximity.ToString();
			this.Temperature = eddystone.Temperature;

            this.Identifier = (eddystone.Type == EddystoneType.Uid)
                 ? $"{eddystone.Namespace}-{eddystone.Instance}"
                 : $"{eddystone.Url}";
        }
    }
}
