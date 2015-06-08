using System;


namespace Estimotes {

    public class BeaconRegion {

        public string Uuid { get; private set; }
        public string Identifier { get; private set; }


        public BeaconRegion(string uuid, string identifier) {
            this.Uuid = uuid;
            this.Identifier = identifier;
        }


        public override bool Equals(object obj) {
            var other = obj as BeaconRegion;
            if (other == null)
                return false;

            if (!Object.Equals(this.Uuid, other.Uuid))
                return false;

            if (!Object.Equals(this.Identifier, other.Identifier))
                return false;

            return true;
        }


        public override int GetHashCode() {
            var hash = this.Uuid.GetHashCode() + this.Identifier.GetHashCode();
            return hash;
        }
    }
}