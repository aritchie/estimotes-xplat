using System;
using System.IO;
using SQLite;
using Samples.Models;


namespace Samples {

	public class SampleDbConnection : SQLiteConnection {

		public SampleDbConnection(string databasePath) : base(Path.Combine(databasePath, "beacons_140.db")) {
			this.CreateTable<BeaconPing>();
			this.BeaconPings = this.Table<BeaconPing>();
		}


		public TableQuery<BeaconPing> BeaconPings { get; }
	}
}