using OnlineRecLeague.Ladders.LadderChallenges;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Ladders
{
	public interface ILadderEvaluator
	{
		void Evaluate(ILadder ladder);
	}

	public class LadderEvaluator : ILadderEvaluator
	{
		public LadderEvaluator(ILadderTeamStore ladderTeamStore = null)
		{
			_ladderTeamStore = ladderTeamStore ?? new LadderTeamStore();
		}

		public void Evaluate(ILadder ladder)
		{
			var allLadderTeams = ladder.AllLadderTeams.OrderBy(x => x.CreatedAtTime);
			var ladderTeamsInLadderOrder = new LinkedList<ILadderTeam>(allLadderTeams);

			var successfulLadderChallenges = ladder.AllLadderChallenges
				.Where(x => x.ChallengeState == LadderChallengeState.Succeeded)
				.OrderBy(x => x.MatchResultsReportedTime)
				.ToList();

			foreach (var ladderChallenge in successfulLadderChallenges)
			{
				ladderTeamsInLadderOrder.Remove(ladderChallenge.ChallengerLadderTeam);

				var challengedLadderRanking = ladderTeamsInLadderOrder.Find(ladderChallenge.ChallengedLadderTeam);
				ladderTeamsInLadderOrder.AddBefore(challengedLadderRanking, ladderChallenge.ChallengerLadderTeam);
			}

			var updateRequests = FindAllLadderRungRequests(ladderTeamsInLadderOrder).ToList();
			if (updateRequests.Any())
			{
				_ladderTeamStore.UpdateLadderRungs(updateRequests);
			}
		}

		private static IEnumerable<UpdateLadderRungRequest> FindAllLadderRungRequests(IEnumerable<ILadderTeam> ladderTeamRanking)
		{
			var rung = 1;

			foreach (var ladderTeam in ladderTeamRanking)
			{
				if (ladderTeam.Rung != rung)
				{
					yield return new UpdateLadderRungRequest { LadderTeamId = ladderTeam.LadderTeamId, CurrentRung = rung };
				}

				rung++;
			}
		}

		private readonly ILadderTeamStore _ladderTeamStore;
	}
}
