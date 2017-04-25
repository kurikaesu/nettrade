using AppKit;
using System.Data;
using Mono.Data.Sqlite;

namespace netTrade
{
	static class MainClass
	{
		static void Main(string[] args)
		{
			IDbConnection dbConn = new SqliteConnection("URI=file:test.db");
			TradeJournalDiaryEntry.CheckAndCreateTables(dbConn);
			TradeJournalDiaryEntry entry = new TradeJournalDiaryEntry(dbConn, 1, 1);
			entry.EntryData = "This is some test";
			entry.Save(dbConn);

			NSApplication.Init();
			NSApplication.Main(args);
		}
	}
}
