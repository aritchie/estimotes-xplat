using System;
using EstimoteSdk;


namespace Estimotes {

    public class ServiceReadyCallbackImpl : Java.Lang.Object, BeaconManager.IServiceReadyCallback {
        private readonly Action callback;


        public ServiceReadyCallbackImpl(Action callback) {
            this.callback = callback;
        }


        public void OnServiceReady() {
            this.callback();
        }
    }
}