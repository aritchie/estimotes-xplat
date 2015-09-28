using System;
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
//            EstimoteManager.Instance.WhenNearables.Subscribe(x => {
//                this.List = x;
//                this.OnPropertyChanged(() => this.List);
//            });
//            EstimoteManager.Instance.StartNearableDiscovery();
//        }
//
//
//        public override void OnDeactivate() {
//            base.OnDeactivate();
//            EstimoteManager.Instance.StopNearableDiscovery();
//        }
//
//
//        public IEnumerable<INearable> List { get; private set; }
//    }
//}
