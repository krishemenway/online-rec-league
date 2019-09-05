using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.Games;
using OnlineRecLeague.Service.DataTypes;
using OnlineRecLeague.Service.LeagueMatchStrategies;
using OnlineRecLeague.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Leagues
{
	public interface ILeagueStore
	{
		ILeague Find(Id<League> leagueId);
		ILeague FindByPath(string path);

		IReadOnlyList<ILeague> FindAll();
		IReadOnlyList<ILeague> FindAll(IGame game);

		ILeague Create(CreateLeagueRequest createLeagueRequest, IUser createdByUser);
	}

	internal class LeagueStore : ILeagueStore
	{
		public ILeague Find(Id<League> leagueId)
		{
			const string sql = @"
				SELECT
					league_id as leagueid,
					name,
					uri_path as uripath,
					game_id,
					created_by_user_id as createdbyuserid,
					rules
				FROM public.league
				WHERE league_id = @LeagueId";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LeagueRecord>(sql, new { leagueId }).Select((record) => new League(record)).Single();
			}
		}

		public ILeague FindByPath(string path)
		{
			const string sql = @"
				SELECT
					league_id as leagueid,
					name,
					uri_path as uripath,
					game_id,
					created_by_user_id as createdbyuserid,
					rules
				FROM public.league
				WHERE uri_path = @Path";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LeagueRecord>(sql, new { path }).Select((record) => new League(record)).Single();
			}
		}

		public IReadOnlyList<ILeague> FindAll()
		{
			const string sql = @"
				SELECT
					league_id as leagueid,
					name,
					uri_path as uripath,
					game_id,
					created_by_user_id as createdbyuserid,
					rules
				FROM public.league";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LeagueRecord>(sql).Select((record) => new League(record)).ToList();
			}
		}

		public IReadOnlyList<ILeague> FindAll(IGame game)
		{
			const string sql = @"
				SELECT
					league_id as leagueid,
					name,
					uri_path as uripath,
					game_id,
					created_by_user_id as createdbyuserid,
					rules
				FROM public.league l
				INNER JOIN public.game s
					ON l.game_id = s.game_id
				WHERE s.game_id = @GameId";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LeagueRecord>(sql, new { game.GameId }).Select((record) => new League(record)).ToList();
			}
		}

		public ILeague Create(CreateLeagueRequest createLeagueRequest, IUser createdByUser)
		{
			const string sql = @"
				INSERT INTO public.league
				(name, uri_path, game_id, rules, created_by_user_id)
				VALUES
				(@Name, @UriPath, @GameId, @Rules, @CreatedByUserId)
				RETURNING league_id;";

			using (var connection = AppDataConnection.Create())
			{
				var sqlParams = new
				{
					createLeagueRequest.Name,
					createLeagueRequest.UriPath,
					createLeagueRequest.GameId,
					createLeagueRequest.Rules,
					createdByUser.UserId,
				};

				return Find(connection.Query<Id<League>>(sql, sqlParams).Single());
			}
		}
	}

	public class LeagueRecord
	{
		public Id<League> LeagueId { get; set; }
		public Id<Game> GameId { get; set; }

		public LeagueMatchStrategies LeagueMatchStrategy { get; set; }

		public string Name { get; set; }
		public string UriPath { get; set;  }

		public string Rules { get; set; }

		public Id<User> CreatedByUserId { get; set; }
	}
}
