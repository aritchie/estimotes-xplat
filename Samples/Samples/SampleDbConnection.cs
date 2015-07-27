using System;
using SQLite;
using Samples.Models;


namespace Samples {
	
	public class SampleDbConnection : SQLiteConnection {
		
		public SampleDbConnection(string databasePath) : base(databasePath) {
			this.CreateTable<BeaconPing>();
			this.BeaconPings = this.Table<BeaconPing>();
		}


		public TableQuery<BeaconPing> BeaconPings { get; }
	}
}