using System;


namespace Estimotes {

    public static class EstimoteManager {
        private static readonly Lazy<IBeaconManager> instanceLoad = new Lazy<IBeaconManager>(() => {
#if __UNIFIED__ || __ANDROID__
            return new BeaconManagerImpl();
#else
            throw new NotSupportedException("");
#endif
        });

        private static IBeaconManager instance;
        public static IBeaconManager Instance {
            get { return instance ?? instanceLoad.Value; }
            set { instance = value; }
        }
    }
}
