using System;


namespace Estimotes {

    public class BeaconRegion {

        public string Uuid { get; }
        public string Identifier { get; }
        public ushort? Major { get; }
        public ushort? Minor { get; }


        public BeaconRegion(string identifier, string uuid, ushort? major = null, ushort? minor = null) {
            this.Identifier = identifier;
            this.Uuid = uuid;
            this.Major = major;
            this.Minor = minor;
        }


        public override bool Equals(object obj) {
            var other = obj as BeaconRegion;
            if (other == null)
                return false;

            if (!Object.Equals(this.Uuid, other.Uuid))
                return false;

            //if (!Object.Equals(this.Identifier, other.Identifier))
            //    return false;

            if (!Object.Equals(this.Major, other.Major))
                return false;

            if (!Object.Equals(this.Minor, other.Minor))
                return false;

            return true;
        }


        public override int GetHashCode() {
            var hash = this.Uuid.GetHashCode() + this.Identifier.GetHashCode();
            return hash;
        }
    }
}