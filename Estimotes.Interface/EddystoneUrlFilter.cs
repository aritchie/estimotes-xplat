using System;

namespace Estimotes {
	
	public class EddystoneUrlFilter : IEddystoneFilter {
		public string Url { get; }
		public bool IsDomain { get; }


		public EddystoneUrlFilter(string url, bool isDomain) {
			this.Url = url;
			this.IsDomain = isDomain;
		}
	}
}

