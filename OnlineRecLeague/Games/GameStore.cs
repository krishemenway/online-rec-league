using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Games
{
	public interface IGameStore
	{
		IGame Save(CreateGameRequest createGameRequest);
		IReadOnlyDictionary<Guid, IGame> FindGamesByGameId(IReadOnlyList<Guid> gameIds);
	}

	public class GameStore : IGameStore
	{
		public IGame Save(CreateGameRequest createGameRequest)
		{
			using (var connection = AppDataConnection.Create())
			{
				const string sql = @"
					INSERT INTO svc.game
					(name)
					VALUES
					(@Name)
					RETURNING game_id;";

				var gameId = connection.Query<Guid>(sql, createGameRequest).Single();
				return FindGamesByGameId(new[] { gameId })[gameId];
			}
		}

		public IReadOnlyDictionary<Guid, IGame> FindGamesByGameId(IReadOnlyList<Guid> gameIds)
		{
			using (var connection = AppDataConnection.Create())
			{
				const string sql = @"
					SELECT
						game_id as gameid,
						name
					FROM svc.game
					WHERE svc.game_id = any(@GameIds)";

				return connection
					.Query<GameRecord>(sql, new { gameIds })
					.Select(CreateGame)
					.ToDictionary(x => x.GameId, x => x);
			}
		}

		public IGame CreateGame(GameRecord gameRecord)
		{
			return new Game
				{
					GameId = gameRecord.GameId,
					Name = gameRecord.Name
				};
		}
	}

	public class CreateGameRequest
	{
		public string Name { get; set; }
	}

	public class GameRecord
	{
		public Guid GameId { get; set; }
		public string Name { get; set; }
	}
}
