using Dapper;
using LeagueService.Ladders.LadderChallenges;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeagueService.Ladders
{
	public interface ILadderChallengeStore
	{
		void Create(SaveLadderChallengeRequest saveLadderChallengeRequest);
		IReadOnlyList<ILadderChallenge> FindAll(ILadder ladder);
		IReadOnlyList<ILadderChallenge> FindAll(ILadderTeam ladderTeam);
	}

	internal class LadderChallengeStore : ILadderChallengeStore
	{
		public LadderChallengeStore(ILadderChallengeStateAnalyzer ladderChallengeStateAnalyzer = null)
		{
			_ladderChallengeStateAnalyzer = ladderChallengeStateAnalyzer ?? new LadderChallengeStateAnalyzer();
		}

		public void Create(SaveLadderChallengeRequest saveLadderChallengeRequest)
		{
			const string sql = @"
				INSERT INTO svc.ladder_challenge
				(ladder_id, challenger_ladder_team_id, challenged_ladder_team_id, challenge_time)
				VALUES 
				(@LadderId, @ChallengerLadderTeamId, @ChallengedLadderTeamId, @ChallengeTime)";

			using (var connection = AppDataConnection.Create())
			{
				connection.Execute(sql, saveLadderChallengeRequest);
			}
		}

		public IReadOnlyList<ILadderChallenge> FindAll(ILadder ladder)
		{
			const string sql = @"
				SELECT
					ladder_challenge_id as ladderchallengeid,
					ladder_id as ladderid,
					challenger_ladder_team_id as challengerladderteamid,
					challenged_ladder_team_id as challengedladderteamid,
					challenge_time as challengetime,
					challenge_successful as challengesuccessful,
					match_results as matchresults,
					match_results_reported_time as matchresultsreportedtime
				FROM svc.ladder_challenge
				WHERE ladder_id = @LadderId";

			using (var connection = AppDataConnection.Create())
			{
				var records = connection.Query<LadderChallengeRecord>(sql, new { ladder.LadderId }).ToList();
				return records.Select(x => Create(x)).ToList();
			}
		}

		public IReadOnlyList<ILadderChallenge> FindAll(ILadderTeam ladderTeam)
		{
			const string sql = @"
				SELECT
					ladder_challenge_id as ladderchallengeid,
					challenger_team_id as challengerladderteamid,
					challenged_team_id as challengedladderteamid,
					match_results as matchresults,
					challenge_successful as challengesuccessful,
					match_results_reported_time as matchresultsreportedtime,
					ladder_id as ladderid,
					challenge_time as challengetime
				FROM svc.ladder_challenge
				WHERE ladder_team_id = @LadderTeamId";

			using (var connection = AppDataConnection.Create())
			{
				var records = connection.Query<LadderChallengeRecord>(sql, new { ladderTeam.LadderTeamId }).ToList();
				return records.Select(x => Create(x)).ToList();
			}
		}

		private ILadderChallenge Create(LadderChallengeRecord record)
		{
			return new LadderChallenge
				{
					LadderChallengeId = record.LadderChallengeId,
					LadderId = record.LadderId,

					ChallengedAtTime = record.ChallengedTime,

					ChallengeState = _ladderChallengeStateAnalyzer.Analyze(record),

					MatchResults = record.MatchResults,
					MatchResultsReportedTime = record.MatchResultsReportedTime
				};
		}

		private readonly ILadderChallengeStateAnalyzer _ladderChallengeStateAnalyzer;
	}

	internal class LadderChallengeRecord
	{
		public Guid LadderChallengeId { get; set; }
		public Guid LadderId { get; set; }

		public Guid ChallengerLadderTeamId { get; set; }
		public Guid ChallengedLadderTeamId { get; set; }

		public DateTime ChallengedTime { get; set; }

		public bool ChallengeSuccessful { get; set; }

		public string MatchResults { get; set; }
		public DateTime? MatchResultsReportedTime { get; set; }
	}

	public class SaveLadderChallengeRequest
	{
		public Guid LadderId { get; set; }
		public Guid ChallengerLadderTeamId { get; set; }
		public Guid ChallengedLadderTeamId { get; set; }
		public DateTime ChallengedTime { get; set; }
	}
}
