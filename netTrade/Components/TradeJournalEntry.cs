using System;
using System.Data;

namespace netTrade
{
	public class TradeJournalEntry : ModelObject
	{
		private long profileId;
		private long entryId;
		private string data;

		private const string checkSql = "SELECT profile_id, entry_id, data FROM TradeJournalEntry";

		public static new void CheckAndCreateTables(IDbConnection conn)
		{

		}

		public TradeJournalEntry(IDbConnection conn, long profileId = 0, long entryId = 0)
		{
			this.profileId = profileId;
			this.entryId = entryId;
		}


	}
}
