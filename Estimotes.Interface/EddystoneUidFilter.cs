using System;

namespace Estimotes {
	
	public class EddystoneUidFilter : IEddystoneFilter {
		public string Namespace { get; }
		public string InstanceId { get; }


		public EddystoneUidFilter(string nameSpace, string instanceId = null) {
			this.Namespace = nameSpace;
			this.InstanceId = instanceId;
		}


		public override string ToString() {
			return this.Namespace + this.InstanceId;
		}


		public override bool Equals(object obj)	{
			var objA = obj as EddystoneUidFilter;
			if (objA == null)
				return false;

			return objA
				.ToString()
				.Equals(
					this.ToString(),
					StringComparison.CurrentCultureIgnoreCase
				);
		}
	}
}

