using System;
using Estimotes;


namespace Samples.ViewModels {

    public class EddystoneViewModel {

        public string Identifier { get; }

        public EddystoneViewModel(IEddystone eddystone) {
            this.Identifier = (eddystone.Type == EddystoneType.Uid)
                 ? $"{eddystone.Namespace}-{eddystone.Instance}"
                 : $"{eddystone.Url}";
        }
    }
}
