using Dapper;
using OnlineRecLeague.Ladders.LadderChallenges;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Ladders
{
	public interface ILadderChallengeStore
	{
		IReadOnlyList<ILadderChallenge> FindAll(ILadder ladder);
		IReadOnlyList<ILadderChallenge> FindAll(ILadderTeam ladderTeam);

		void Create(SaveLadderChallengeRequest saveLadderChallengeRequest);
		void SetMatchTime(ILadderChallenge challenge, DateTime matchTime);
	}

	internal class LadderChallengeStore : ILadderChallengeStore
	{
		public LadderChallengeStore(ILadderChallengeStateAnalyzer ladderChallengeStateAnalyzer = null)
		{
			_ladderChallengeStateAnalyzer = ladderChallengeStateAnalyzer ?? new LadderChallengeStateAnalyzer();
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
					match_time as matchtime,
					challenge_successful as challengesuccessful,
					match_results as matchresults,
					match_results_reported_time as matchresultsreportedtime
				FROM svc.ladder_challenge
				WHERE ladder_id = @LadderId";

			using (var connection = AppDataConnection.Create())
			{
				var records = connection.Query<LadderChallengeRecord>(sql, new { ladder.LadderId }).ToList();
				var ladderTeamIds = records.SelectMany(x => new[] { x.ChallengedLadderTeamId, x.ChallengerLadderTeamId }).ToList();
				var ladderTeamsByLadderTeamId = new LadderTeamStore().Find(ladderTeamIds).ToDictionary(x => x.LadderTeamId, x => x);

				return records.Select(x => Create(x, ladderTeamsByLadderTeamId)).ToList();
			}
		}

		public IReadOnlyList<ILadderChallenge> FindAll(ILadderTeam ladderTeam)
		{
			const string sql = @"
				SELECT
					ladder_challenge_id as ladderchallengeid,
					challenger_team_id as challengerladderteamid,
					challenged_team_id as challengedladderteamid,
					challenge_time as challengetime,
					match_results as matchresults,
					match_time as matchtime,
					challenge_successful as challengesuccessful,
					match_results_reported_time as matchresultsreportedtime,
					ladder_id as ladderid
				FROM svc.ladder_challenge
				WHERE ladder_team_id = @LadderTeamId";

			using (var connection = AppDataConnection.Create())
			{
				var records = connection.Query<LadderChallengeRecord>(sql, new { ladderTeam.LadderTeamId }).ToList();
				var ladderTeamIds = records.SelectMany(x => new[] { x.ChallengedLadderTeamId, x.ChallengerLadderTeamId }).ToList();
				var ladderTeamsByLadderTeamId = new LadderTeamStore().Find(ladderTeamIds).ToDictionary(x => x.LadderTeamId, x => x);

				return records.Select(x => Create(x, ladderTeamsByLadderTeamId)).ToList();
			}
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

		public void SetMatchTime(ILadderChallenge challenge, DateTime matchTime)
		{
			const string sql = @"
				UPDATE svc.ladder_challenge
				SET match_time = @MatchTime
				WHERE ladder_challenge_id = @LadderChallengeId";

			using (var connection = AppDataConnection.Create())
			{
				connection.Execute(sql, new { challenge.LadderChallengeId, matchTime });
			}
		}

		private ILadderChallenge Create(LadderChallengeRecord record, IReadOnlyDictionary<Guid, ILadderTeam> ladderTeamsByLadderTeamId)
		{
			return new LadderChallenge
				{
					LadderChallengeId = record.LadderChallengeId,
					LadderId = record.LadderId,

					ChallengedAtTime = record.ChallengedTime,

					ChallengedLadderTeam = ladderTeamsByLadderTeamId[record.ChallengedLadderTeamId],
					ChallengerLadderTeam = ladderTeamsByLadderTeamId[record.ChallengerLadderTeamId],

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
		public DateTime? MatchTime { get; set; }

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
