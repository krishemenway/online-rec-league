using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.Games;
using OnlineRecLeague.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Ladders
{
	public interface ILadderStore
	{
		ILadder Find(Guid ladderId);
		ILadder FindByPath(string path);

		IReadOnlyList<ILadder> FindAll();
		IReadOnlyList<ILadder> FindAll(IGame game);

		ILadder Create(CreateLadderRequest createLadderRequest, IUser createdByUser);
	}

	internal class LadderStore : ILadderStore
	{
		public ILadder Find(Guid ladderId)
		{
			const string sql = @"
				SELECT
					ladder_id as ladderid,
					name,
					uri_path as uripath,
					sport_id,
					created_by_user_id as createdbyuserid,
					rules
				FROM public.ladder
				WHERE ladder_id = @LadderId";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LadderRecord>(sql, new { ladderId }).Select((record) => new Ladder(record)).Single();
			}
		}

		public ILadder FindByPath(string path)
		{
			const string sql = @"
				SELECT
					ladder_id as ladderid,
					name,
					uri_path as uripath,
					sport_id,
					created_by_user_id as createdbyuserid,
					rules
				FROM public.ladder
				WHERE uri_path = @Path";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LadderRecord>(sql, new { path }).Select((record) => new Ladder(record)).Single();
			}
		}

		public IReadOnlyList<ILadder> FindAll()
		{
			const string sql = @"
				SELECT
					ladder_id as ladderid,
					name,
					uri_path as uripath,
					sport_id,
					created_by_user_id as createdbyuserid,
					rules
				FROM public.ladder";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LadderRecord>(sql).Select((record) => new Ladder(record)).ToList();
			}
		}

		public IReadOnlyList<ILadder> FindAll(IGame game)
		{
			const string sql = @"
				SELECT
					ladder_id as ladderid,
					name,
					uri_path as uripath,
					sport_id,
					created_by_user_id as createdbyuserid,
					rules
				FROM public.ladder l
				INNER JOIN public.sport s
					ON l.sport_id = s.sport_id
				WHERE s.game_id = @GameId";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LadderRecord>(sql, new { game.GameId }).Select((record) => new Ladder(record)).ToList();
			}
		}

		public ILadder Create(CreateLadderRequest createLadderRequest, IUser createdByUser)
		{
			const string sql = @"
				INSERT INTO public.ladder
				(name, uri_path, sport_id, rules, created_by_user_id)
				VALUES
				(@Name, @UriPath, @SportId, @Rules, @CreatedByUserId)
				RETURNING ladder_id;";

			using (var connection = AppDataConnection.Create())
			{
				var sqlParams = new
				{
					createLadderRequest.Name,
					createLadderRequest.UriPath,
					createLadderRequest.SportId,
					createLadderRequest.Rules,
					createdByUser.UserId,
				};

				var ladderId = connection.Query<Guid>(sql, sqlParams).Single();
				return Find(ladderId);
			}
		}
	}

	public class LadderRecord
	{
		public Guid LadderId { get; set; }
		public Guid SportId { get; set; }

		public string Name { get; set; }
		public string UriPath { get; set;  }

		public string Rules { get; set; }

		public Guid CreatedByUserId { get; set; }
	}
}
