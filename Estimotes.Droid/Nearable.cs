using System;


namespace Estimotes {

    public class Nearable : INearable {
        private readonly EstimoteSdk.Nearable native;


        public Nearable(EstimoteSdk.Nearable native)  {
            this.native = native;
        }


        public string Identifier => this.native.Identifier;
    }
}
