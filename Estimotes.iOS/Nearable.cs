using System;


namespace Estimotes {

    public class Nearable : INearable {
        readonly Estimote.Nearable native;


        public Nearable(Estimote.Nearable native) {
            this.native = native;
        }


        public string Identifier => this.native.Identifier;
        public TimeSpan CurrentMotionDuration => TimeSpan.FromMilliseconds(this.native.CurrentMotionStateDuration);
        public double Temperature => this.native.Temperature;
        public bool IsMoving => this.native.IsMoving;
        public int Rssi => (int)this.native.Rssi;
    }
}
