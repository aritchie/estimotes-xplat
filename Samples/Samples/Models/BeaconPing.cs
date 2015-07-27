using System;
using SQLite;


namespace Samples.Models {
	
	public class BeaconPing {

		[AutoIncrement]
		[PrimaryKey]
		public int Id { get; set; }
		public string Identifier { get; set; }
		public string Uuid { get; set; }
		public ushort Major { get; set; }
		public ushort Minor { get; set; }
		public bool IsAppInBackground { get; set; }
		public bool IsEntering { get; set; }
		public DateTime DateCreated { get; set; }
	}
}