using Dapper;
using OnlineRecLeague.AppData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Games
{
	public interface IGameStore
	{
		IGame Save(CreateGameRequest createGameRequest);
		IReadOnlyDictionary<Guid, IGame> FindGamesByGameId(params Guid[] gameIds);
	}

	internal class GameStore : IGameStore
	{
		public IGame Save(CreateGameRequest createGameRequest)
		{
			using (var connection = AppDataConnection.Create())
			{
				const string sql = @"
					INSERT INTO svc.game
					(name, release_date)
					VALUES
					(@Name, @ReleaseDate)
					RETURNING game_id;";

				var gameId = connection.Query<Guid>(sql, createGameRequest).Single();
				return FindGamesByGameId(new[] { gameId })[gameId];
			}
		}

		public IReadOnlyDictionary<Guid, IGame> FindGamesByGameId(params Guid[] gameIds)
		{
			using (var connection = AppDataConnection.Create())
			{
				const string sql = @"
					SELECT
						game_id as gameid,
						name,
						release_date as releasedate,
					FROM svc.game
					WHERE svc.game_id = any(@GameIds)";

				return connection
					.Query<GameRecord>(sql, new { gameIds })
					.Select(CreateGame)
					.ToDictionary(x => x.GameId, x => x);
			}
		}

		private IGame CreateGame(GameRecord gameRecord)
		{
			return new Game
				{
					GameId = gameRecord.GameId,
					Name = gameRecord.Name,
					ReleaseDate = gameRecord.ReleaseDate,
				};
		}
	}
}
