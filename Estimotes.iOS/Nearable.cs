using System;


namespace Estimotes {

    public class Nearable : INearable {
        private readonly Estimote.Nearable native;


        public Nearable(Estimote.Nearable native)  {
            this.native = native;
        }


        public string Identifier => this.native.Identifier;
    }
}
