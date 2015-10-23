using System;


namespace Estimotes {

    public static class EstimoteManager {
        static readonly Lazy<IBeaconManager> instanceLoad = new Lazy<IBeaconManager>(() => {
#if __UNIFIED__ || __ANDROID__
            return new BeaconManagerImpl();
#else
            throw new NotSupportedException("This is the PCL library, not the platform library.  You must install the nuget package in your main executable/application project");
#endif
        });


        static IBeaconManager instance;
        public static IBeaconManager Instance {
            get { return instance ?? instanceLoad.Value; }
            set { instance = value; }
        }
    }
}
