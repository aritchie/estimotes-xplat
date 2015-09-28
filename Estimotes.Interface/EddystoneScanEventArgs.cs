using System;
using System.Collections.Generic;


namespace Estimotes {
	
	public class EddystoneScanEventArgs : EventArgs {		
		public IEnumerable<IEddystone> Eddystones { get; }
		public IEddystoneFilter Filter { get; }


		public EddystoneScanEventArgs(IEddystoneFilter filter, IEnumerable<IEddystone> eddystones) {
			this.Filter = filter;
			this.Eddystones = eddystones;
		}
	}
}