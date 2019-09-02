using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.Games;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Sports
{
	public interface ISportStore
	{
		IReadOnlyDictionary<Guid, ISport> FindById(params Guid[] sportIds);
	}

	public class SportStore : ISportStore
	{
		public SportStore(IGameStore gameStore = null)
		{
			_gameStore = gameStore ?? new GameStore();
		}

		public IReadOnlyDictionary<Guid, ISport> FindById(params Guid[] sportIds)
		{
			const string sql = @"
				SELECT
					sport_id as sportid,
					name,
					game_id as gameid
				FROM
					public.sport
				WHERE
					sport_id = any(@SportIds)";

			using (var connection = AppDataConnection.Create())
			{
				var sportRecords = connection.Query<SportRecord>(sql, new { sportIds }).ToList();
				var gamesByGameId = _gameStore.FindGamesByGameId(sportRecords.Select(sport => sport.GameId).ToArray());

				return sportRecords.ToDictionary((record) => record.SportId, (record) => CreateSport(record, () => gamesByGameId[record.GameId]));
			}
		}

		private ISport CreateSport(SportRecord record, Func<IGame> findGameFunc)
		{
			return new Sport(record.SportId, findGameFunc)
				{
					Name = record.Name
				};
		}

		private readonly IGameStore _gameStore;
	}
}
