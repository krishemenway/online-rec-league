using LeagueService.Ladders.LadderChallenges;
using System.Collections.Generic;
using System.Linq;

namespace LeagueService.Ladders
{
	public interface ILadderRankingEvaluator
	{
		void EvaluateLadderTeamRankings(ILadder ladder);
	}

	public class LadderRankingEvaluator : ILadderRankingEvaluator
	{
		public LadderRankingEvaluator(ILadderTeamStore ladderTeamStore = null)
		{
			_ladderTeamStore = ladderTeamStore ?? new LadderTeamStore();
		}

		public void EvaluateLadderTeamRankings(ILadder ladder)
		{
			var ladderTeamRanking = new LinkedList<ILadderTeam>(ladder.AllLadderTeams.OrderBy(x => x.CreatedAtTime));

			var successfulLadderChallenges = ladder.AllLadderChallenges
				.Where(x => x.ChallengeState == LadderChallengeState.Succeeded)
				.OrderBy(x => x.MatchResultsReportedTime);

			foreach (var ladderChallenge in successfulLadderChallenges)
			{
				ladderTeamRanking.Remove(ladderChallenge.ChallengerLadderTeam);

				var challengedLadderRanking = ladderTeamRanking.Find(ladderChallenge.ChallengedLadderTeam);
				ladderTeamRanking.AddBefore(challengedLadderRanking, ladderChallenge.ChallengerLadderTeam);
			}

			var updateRankingRequests = new List<UpdateLadderRankRequest>();
			var rank = 1;
			foreach (var ladderTeam in ladderTeamRanking)
			{
				if(ladderTeam.Ranking != rank)
				{
					updateRankingRequests.Add(new UpdateLadderRankRequest { LadderTeamId = ladderTeam.LadderTeamId, NewRank = rank });
				}

				rank++;
			}

			if(updateRankingRequests.Any())
			{
				_ladderTeamStore.UpdateLadderRanks(updateRankingRequests);
			}
		}

		private readonly ILadderTeamStore _ladderTeamStore;
	}
}
