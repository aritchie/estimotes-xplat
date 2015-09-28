//using System;
//
//
//namespace Estimotes {
//
//    public class Nearable : INearable {
//        private readonly EstimoteSdk.Nearable native;
//
//
//        public Nearable(EstimoteSdk.Nearable native) {
//            this.native = native;
//        }
//
//
//        public string Identifier => this.native.Identifier;
//        public TimeSpan CurrentMotionDuration => TimeSpan.FromMilliseconds(this.native.CurrentMotionStateDuration);
//        public double Temperature => this.native.Temperature;
//        public bool IsMoving => this.native.IsMoving;
//        public int Rssi => this.native.Rssi;
//    }
//}
