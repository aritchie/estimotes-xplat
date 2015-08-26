using System;


namespace Estimotes {

    public abstract class AbstractBeacon : IBeacon {


		protected AbstractBeacon(string uuid, Proximity proximity, ushort major, ushort minor) {
			this.Uuid = uuid;
            this.Proximity = proximity;
            this.Major = major;
			this.Minor = minor;
        }


        public ushort Major { get; }
        public ushort Minor { get; }
        public Proximity Proximity { get; }
        public string Uuid { get; }


        public override bool Equals(object obj) {
            var other = obj as IBeacon;
            if (other == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var flag = this.Uuid == other.Uuid && this.Major == other.Major && this.Minor == other.Minor;
            return flag;
        }


        public override int GetHashCode() {
            return this.Uuid.GetHashCode() + this.Major.GetHashCode() + this.Minor.GetHashCode();
        }


        public override string ToString() {
            return $"[UUID: {this.Uuid} - Major: {this.Major} - Minor: {this.Minor}]";
        }
    }
}