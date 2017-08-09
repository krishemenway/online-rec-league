using Dapper;
using System;
using System.Linq;

namespace LeagueService.Ladders
{
	public interface ILadderStore
	{
		ILadder Find(Guid ladderId);
	}

	public class LadderStore : ILadderStore
	{
		public ILadder Find(Guid ladderId)
		{
			const string sql = @"
				SELECT
					ladder_id as ladderid,
					name,
					uri_path as uripath,
					esport_id,
					forces_real_names as forcesrealnames
				FROM svc.ladder
				WHERE ladder_id = @LadderId";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<LadderRecord>(sql, new { ladderId }).Select(Create).Single();
			}
		}

		private ILadder Create(LadderRecord record)
		{
			return new Ladder
				{
					LadderId = record.LadderId,
					Name = record.Name,
					UriPath = record.UriPath,
					EsportId = record.EsportId,
					ForcesRealNames = record.ForcesRealNames
				};
		}
	}

	public class LadderRecord
	{
		public Guid LadderId { get; set; }
		public Guid EsportId { get; set; }

		public string Name { get; set; }
		public string UriPath { get; set;  }

		public bool ForcesRealNames { get; set; }
	}
}
