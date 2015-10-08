using System;

namespace Estimotes {

	public class EddystoneUrlFilter : IEddystoneFilter {
		public string Url { get; }
		public bool IsDomain { get; }


		public EddystoneUrlFilter(string url, bool isDomain) {
			this.Url = url.ToLower();
			this.IsDomain = isDomain;
		}


		public override string ToString() {
			return this.Url + this.IsDomain;
		}


		public override bool Equals(object obj) {
			var objA = obj as EddystoneUrlFilter;
			if (objA == null)
				return false;

			return objA
				.ToString()
				.Equals(
					this.ToString(),
					StringComparison.CurrentCultureIgnoreCase
				);
		}


	    public override int GetHashCode() {
	        return this.Url.GetHashCode();
	    }
	}
}