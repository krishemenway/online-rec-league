using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.Games;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Ladders
{
	public interface ILadderStore
	{
		ILadder Find(Guid ladderId);

		IReadOnlyList<ILadder> FindAll();
		IReadOnlyList<ILadder> FindAll(IGame game);

		ILadder Create(CreateLadderRequest createLadderRequest);
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
					rules
				FROM svc.ladder
				WHERE ladder_id = @LadderId";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LadderRecord>(sql, new { ladderId }).Select((record) => new Ladder(record)).Single();
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
					rules
				FROM svc.ladder";

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
					rules
				FROM svc.ladder l
				INNER JOIN svc.sport s
					ON l.sport_id = s.sport_id
				WHERE s.game_id = @GameId";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LadderRecord>(sql, new { game.GameId }).Select((record) => new Ladder(record)).ToList();
			}
		}

		public ILadder Create(CreateLadderRequest createLadderRequest)
		{
			const string sql = @"
				INSERT INTO svc.ladder
				(name, uri_path, sport_id, rules)
				VALUES
				(@Name, @UriPath, @SportId, @Rules)
				RETURNING ladder_id;";

			using (var connection = AppDataConnection.Create())
			{
				return Find(connection.Query<Guid>(sql, createLadderRequest).Single());
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
	}
}
