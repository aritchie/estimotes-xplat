using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Acr;
using Estimotes;


namespace Samples.ViewModels {

    public class EddystoneListViewModel : LifecycleViewModel {
		readonly IEddystoneFilter filter = new EddystoneUidFilter("EDD1EBEAC04E5DEFA017");

		//EDD1EBEAC04E5DEFA017
		//cd5e3f3ec33a
		//		EddystoneFilter filter = new EddystoneFilterUID(new EddystoneUID("EDD1EBEAC04E5DEFA017"));

        public override void OnActivate() {
            base.OnActivate();
            EstimoteManager.Instance.WhenEddystone.Subscribe(x => {
				try {
					this.List = x.Select(y => new EddystoneViewModel(y)).ToList();
                	this.OnPropertyChanged(() => this.List);
				}
				catch (Exception ex) {
					Debug.WriteLine("[eddystone]: {0}", ex);
				}
            });
			EstimoteManager.Instance.StartEddystoneScan(this.filter);
        }


        public override void OnDeactivate() {
            base.OnDeactivate();
			EstimoteManager.Instance.StopEddystoneScan(this.filter);
        }


        public IList<EddystoneViewModel> List { get; private set; }
    }
}
