using Dapper;
using OnlineRecLeague.AppData;
using System;
using System.Linq;

namespace OnlineRecLeague.Leagues
{
	public interface ILeagueStore
	{
		ILeague FindById(params Guid[] leagueId);
		ILeague Create(CreateLeagueRequest request);
	}

	public class LeagueStore : ILeagueStore
	{
		public ILeague FindById(params Guid[] leagueId)
		{
			const string sql = @"
				SELECT
					league_id as leagueid,
					name,
					rules
				FROM svc.league
				WHERE league_id = @LeagueId";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LeagueRecord>(sql, new { leagueId }).Select((record) => new League(record)).Single();
			}
		}

		public ILeague Create(CreateLeagueRequest request)
		{
			const string sql = @"
				INSERT INTO svc.league
				(name, uri_path, sport_id, rules)
				VALUES
				(@Name, @UriPath, @SportId, @Rules)
				RETURNING league_id;";

			using (var connection = AppDataConnection.Create())
			{
				return FindById(connection.Query<Guid>(sql, request).Single());
			}
		}
	}

	public class LeagueRecord
	{
		public Guid LeagueId { get; set; }
		public string Name { get; set; }
		public string Rules { get; set; }
	}
}
