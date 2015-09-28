using System;
using System.Collections.Generic;


namespace Estimotes {
	
	public class EddystoneScanEventArgs : EventArgs {		
		public IEnumerable<IEddystone> Eddystones { get; }
		public EddystoneFilter Filter { get; }


		public EddystoneScanEventArgs(EddystoneFilter filter, IEnumerable<IEddystone> eddystones) {
			this.Filter = filter;
			this.Eddystones = eddystones;
		}
	}
}