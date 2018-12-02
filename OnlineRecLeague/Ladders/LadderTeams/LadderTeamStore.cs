using Dapper;
using OnlineRecLeague.Teams;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Ladders
{
	public interface ILadderTeamStore
	{
		IReadOnlyList<ILadderTeam> Find(ILadder ladder);

		void UpdateLadderRungs(IReadOnlyList<UpdateLadderRungRequest> updateLadderRankRequests);
	}

	public class LadderTeamStore : ILadderTeamStore
	{
		public LadderTeamStore(ITeamStore teamStore = null)
		{
			_teamStore = teamStore ?? new TeamStore();
		}

		public IReadOnlyList<ILadderTeam> Find(IReadOnlyList<Guid> ladderTeamIds)
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

			using (var connection = Database.CreateConnection())
			{
				var records = connection.Query<LadderTeamRecord>(sql, new { ladderTeamIds }).ToList();
				var teamByTeamId = _teamStore.FindTeams(records.Select(x => x.TeamId).ToList()).ToDictionary(x => x.TeamId, x => x);

				return records.Select(record => CreateLadderTeam(record, teamByTeamId[record.TeamId])).ToList();
			}
		}

		public IReadOnlyList<ILadderTeam> Find(ILadder ladder)
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

			using (var connection = Database.CreateConnection())
			{
				var records = connection.Query<LadderTeamRecord>(sql, new { ladder.LadderId }).ToList();
				var teamByTeamId = _teamStore.FindTeams(records.Select(x => x.TeamId).ToList()).ToDictionary(x => x.TeamId, x => x);

				return records.Select(record => CreateLadderTeam(record, teamByTeamId[record.TeamId])).ToList();
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

			using (var connection = Database.CreateConnection())
			{
				var sqlParams = new
					{
						LadderTeamIds = updateLadderRungsRequests.Select(rungRequest => rungRequest.LadderTeamId),
						NewRungs = updateLadderRungsRequests.Select(rungRequest => rungRequest.CurrentRung)
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
					Rung = record.Ranking,
					CreatedAtTime = record.CreatedAtTime
				};
		}

		private ITeamStore _teamStore;
	}

	public class LadderTeamRecord
	{
		public Guid LadderTeamId { get; set; }
		public Guid TeamId { get; set; }
		public int Ranking { get; set; }
		public DateTimeOffset CreatedAtTime { get; set; }
		public Guid CreatedByUserId { get; set; }
	}

	public class UpdateLadderRungRequest
	{
		public Guid LadderTeamId { get; set; }
		public int CurrentRung { get; set; }
	}
}
