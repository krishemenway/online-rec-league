using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.Service.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Games
{
	public interface IGameStore
	{
		IGame Save(CreateGameRequest createGameRequest);
		IReadOnlyDictionary<Id<Game>, IGame> FindGamesByGameId(params Id<Game>[] gameIds);
	}

	internal class GameStore : IGameStore
	{
		public IGame Save(CreateGameRequest createGameRequest)
		{
			using (var connection = AppDataConnection.Create())
			{
				const string sql = @"
					INSERT INTO public.game
					(name, release_date)
					VALUES
					(@Name, @ReleaseDate)
					RETURNING game_id;";

				var gameId = connection.Query<Id<Game>>(sql, createGameRequest).Single();
				return FindGamesByGameId(new[] { gameId })[gameId];
			}
		}

		public IReadOnlyDictionary<Id<Game>, IGame> FindGamesByGameId(params Id<Game>[] gameIds)
		{
			using (var connection = AppDataConnection.Create())
			{
				const string sql = @"
					SELECT
						game_id as gameid,
						name,
						release_date as releasedate,
					FROM public.game
					WHERE public.game_id = any(@GameIds)";

				return connection
					.Query<GameRecord>(sql, new { GameIds = gameIds.ConvertToGuids().ToList() })
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

	public class GameRecord
	{
		public Id<Game> GameId { get; set; }
		public string Name { get; set; }
		public DateTimeOffset ReleaseDate { get; set; }
	}
}
