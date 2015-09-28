using System;

namespace Estimotes {
	
	public class EddystoneUidFilter : IEddystoneFilter {
		public string Namespace { get; }
		public string InstanceId { get; }


		public EddystoneUidFilter(string nameSpace, string instanceId = null) {
			this.Namespace = nameSpace;
			this.InstanceId = instanceId;
		}


		public override string ToString () {
			return $"[EddystoneUidFilter: Namespace={this.Namespace}, InstanceId={this.InstanceId}]";
		}
	}
}

