using System;
using System.Collections.Generic;
using Acr;
using Estimotes;


namespace Samples.ViewModels {

    public class EddystoneViewModel : LifecycleViewModel {

        public override void OnActivate() {
            base.OnActivate();
            EstimoteManager.Instance.WhenEddystone.Subscribe(x => {
                this.List = x;
                this.OnPropertyChanged(() => this.List);
            });
            EstimoteManager.Instance.StartEddystoneScan();
        }


        public override void OnDeactivate() {
            base.OnDeactivate();
            EstimoteManager.Instance.StopEddystoneScan();
        }


        public IEnumerable<IEddystone> List { get; private set; }
    }
}
