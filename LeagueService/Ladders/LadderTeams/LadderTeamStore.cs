using Dapper;
using LeagueService.Teams;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeagueService.Ladders
{
	public interface ILadderTeamStore
	{
		IReadOnlyList<ILadderTeam> FindAllTeams(ILadder ladder);
		void UpdateLadderRanks(IReadOnlyList<UpdateLadderRankRequest> updateLadderRankRequests);
	}

	public class LadderTeamStore : ILadderTeamStore
	{
		public IReadOnlyList<ILadderTeam> FindAllTeams(ILadder ladder)
		{
			const string sql = @"
				SELECT
					ladder_team_id as ladderteamid,
					team_id as teamid,
					ranking,
					created_at_time as createdattime,
					created_by_user_id as createdbyuserid
				FROM svc.ladder_team
				WHERE ladder_id = @LadderId";

			using (var connection = AppDataConnection.Create())
			{
				var records = connection.Query<LadderTeamRecord>(sql, new { ladder.LadderId }).ToList();
				var teamByTeamId = new TeamStore().FindTeams(records.Select(x => x.TeamId).ToList()).ToDictionary(x => x.TeamId, x => x);
				return records.Select(x => CreateLadderTeam(x, teamByTeamId[x.TeamId])).ToList();
			}
		}

		public void UpdateLadderRanks(IReadOnlyList<UpdateLadderRankRequest> updateLadderRankRequests)
		{
			throw new NotImplementedException();
		}

		public ILadderTeam CreateLadderTeam(LadderTeamRecord record, ITeam team)
		{
			return new LadderTeam(record.LadderTeamId)
				{
					Team = team,
					CreatedByUserId = record.CreatedByUserId,
					Ranking = record.Ranking,
					CreatedAtTime = record.CreatedAtTime
				};
		}
	}

	public class LadderTeamRecord
	{
		public Guid LadderTeamId { get; set; }
		public Guid TeamId { get; set; }
		public int Ranking { get; set; }
		public DateTime CreatedAtTime { get; set; }
		public Guid CreatedByUserId { get; set; }
	}

	public class UpdateLadderRankRequest
	{
		public Guid LadderTeamId { get; set; }
		public int NewRank { get; set; }
	}
}
