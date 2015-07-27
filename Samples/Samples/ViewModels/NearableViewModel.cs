//using System;
//using System.Collections.Generic;
//using Acr;
//using Estimotes;
//
//
//namespace Samples.ViewModels {
//
//    public class NearableViewModel : LifecycleViewModel {
//
//        public override void OnActivate() {
//            base.OnActivate();
//            EstimoteManager.Instance.Nearables += this.OnNearables;
//            //EstimoteManager.Instance.StartNearableDiscovery();
//        }
//
//
//        public override void OnDeactivate() {
//            base.OnDeactivate();
//            EstimoteManager.Instance.Nearables -= this.OnNearables;
//            //EstimoteManager.Instance.StopNearableDiscovery();
//        }
//
//
//        private void OnNearables(object sender, IEnumerable<INearable> list) {
//            this.List = list;
//            this.OnPropertyChanged(() => this.List);
//        }
//
//
//        public IEnumerable<INearable> List { get; private set; }
//    }
//}
