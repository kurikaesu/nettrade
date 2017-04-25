using System;
using System.Data;
using System.Data.Common;
using Mono.Data.Sqlite;

namespace netTrade
{
	public class TradeJournalDiaryEntry : ModelObject
	{
		private long entryId;
		private long profileId;
		private string entryData;

		private const string checkSql = "SELECT profile_id, entry_id, data FROM TradeJournalDiaryEntry";
		private const string createSql = "CREATE TABLE TradeJournalDiaryEntry (profile_id INTEGER " +
			"NOT NULL, entry_id INTEGER NOT NULL, data TEXT)";
		private const string getSql = "SELECT data FROM TradeJournalDiaryEntry WHERE " +
			"profile_id=@profileId AND entry_id=@entryId";

		private const string insertSql = "INSERT INTO TradeJournalDiaryEntry (profile_id, entry_id, " +
			"data) VALUES (@profileId, @entryId, @data)";

		private const string updateSql = "UPDATE TradeJournalDiaryEntry SET data=@data WHERE " +
			"profile_id=@profileId AND entry_id=@entryId";

		private const string lastEntryIdSql = "SELECT MAX(entryId) FROM TradeJournalDiaryEntry " +
			"WHERE profileId=@profileId";

		// Prepared statement parameters
		private IDbDataParameter profileParam;
		private IDbDataParameter entryParam;
		private IDbDataParameter dataParam;

		public static new void CheckAndCreateTables(IDbConnection conn)
		{
			conn.Open();

			IDbCommand dbcmd = conn.CreateCommand();
			dbcmd.CommandText = checkSql;
			try
			{
				IDataReader reader = dbcmd.ExecuteReader();
				reader.Close();
			}
			catch (DbException)
			{
				dbcmd.CommandText = createSql;
				dbcmd.ExecuteNonQuery();
			}

			conn.Close();
		}

		public TradeJournalDiaryEntry(IDbConnection conn, uint profileId = 0, uint entryId = 0)
		{
			this.profileId = profileId;
			this.entryId = entryId;

			// Open the connection
			conn.Open();
			IDbCommand dbcmd = conn.CreateCommand();
			dbcmd.CommandText = getSql;

			// Profile Parameter
			profileParam = dbcmd.CreateParameter();
			profileParam.ParameterName = "@profileId";
			profileParam.DbType = DbType.Int64;
			profileParam.Value = profileId;
			dbcmd.Parameters.Add(profileParam);

			// Entry Parameter
			entryParam = dbcmd.CreateParameter();
			entryParam.ParameterName = "@entryId";
			entryParam.DbType = DbType.Int64;
			entryParam.Value = entryId;
			dbcmd.Parameters.Add(entryParam);

			// Data Parameter
			dataParam = dbcmd.CreateParameter();
			dataParam.ParameterName = "@data";
			dataParam.DbType = DbType.String;

			dbcmd.Prepare();
			IDataReader reader = dbcmd.ExecuteReader();
			while (reader.Read())
			{
				this.entryData = ModelUtilities.UnpackData(reader.GetString(0));
			}
			reader.Close();

			// Close the connection
			conn.Close();
		}

		public string EntryData
		{
			get
			{
				return entryData;
			}
			set
			{
				entryData = value;
			}
		}

		public bool Save(IDbConnection conn)
		{
			dataParam.Value = ModelUtilities.PackData(this.entryData);
			if (profileId != 0)
			{
				conn.Open();

				if (entryId != 0)
				{
					profileParam.Value = profileId;
					entryParam.Value = entryId;

					IDbCommand cmd = conn.CreateCommand();
					cmd.CommandText = updateSql;
					cmd.Parameters.Add(profileParam);
					cmd.Parameters.Add(entryParam);
					cmd.Parameters.Add(dataParam);

					cmd.Prepare();
					cmd.ExecuteNonQuery();
				}
				else
				{
					profileParam.Value = profileId;
					IDbCommand cmd = conn.CreateCommand();
					cmd.CommandText = lastEntryIdSql;
					cmd.Parameters.Add(profileParam);
					cmd.Prepare();

					IDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						entryId = reader.GetInt64(0) + 1;
					}
					reader.Close();

					entryParam.Value = entryId;
					cmd.CommandText = insertSql;
					cmd.Parameters.Add(profileParam);
					cmd.Parameters.Add(entryParam);
					cmd.Parameters.Add(dataParam);

					cmd.Prepare();
					cmd.ExecuteNonQuery();
				}

				conn.Close();
				return true;
			}

			return false;
		}
	}
}
