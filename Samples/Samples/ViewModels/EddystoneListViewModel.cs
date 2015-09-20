using System;
using System.Collections.Generic;
using System.Linq;
using Acr;
using Estimotes;


namespace Samples.ViewModels {

    public class EddystoneListViewModel : LifecycleViewModel {

        public override void OnActivate() {
            base.OnActivate();
            EstimoteManager.Instance.WhenEddystone.Subscribe(x => {
                this.List = x.Select(y => new EddystoneViewModel(y)).ToList();
                this.OnPropertyChanged(() => this.List);
            });
            EstimoteManager.Instance.StartEddystoneScan();
        }


        public override void OnDeactivate() {
            base.OnDeactivate();
            EstimoteManager.Instance.StopEddystoneScan();
        }


        public IList<EddystoneViewModel> List { get; private set; }
    }
}
