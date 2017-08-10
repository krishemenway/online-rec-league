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
		void UpdateLadderRungs(IReadOnlyList<UpdateLadderRungRequest> updateLadderRankRequests);
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

		public void UpdateLadderRungs(IReadOnlyList<UpdateLadderRungRequest> updateLadderRungsRequests)
		{
			var sql = $@"
				START TRANSACTION;

				CREATE TEMP TABLE ladder_team_rungs 
				(ladder_team_id uuid, current_rung bigint);

				-- elaborate all the parameters in array: @LadderTeamIds @NewRungs

				INSERT INTO ladder_team_rungs
				(ladder_team_id, current_rung)
				VALUES
				{updateLadderRungsRequests.Select((x,i) => $"(@LadderTeamIds{i+1}, @NewRungs{i+1}),")}

				UPDATE svc.ladder_team
				SET current_rung = ltr.current_rung
				FROM svc.ladder_team AS lt
				INNER JOIN ladder_team_rungs AS ltr 
					ON ltr.ladder_team_id = lt.ladder_team_id;

				ROLLBACK;";

			using (var connection = AppDataConnection.Create())
			{
				var sqlParams = new
					{
						LadderTeamIds = updateLadderRungsRequests.Select(x => x.LadderTeamId),
						NewRungs = updateLadderRungsRequests.Select(x => x.CurrentRung)
					};

				connection.Execute(sql, sqlParams);
			}
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

	public class UpdateLadderRungRequest
	{
		public Guid LadderTeamId { get; set; }
		public int CurrentRung { get; set; }
	}
}
