using OnlineRecLeague.Ladders.LadderChallenges;
using System;

namespace OnlineRecLeague.Ladders
{
	public interface ILadderChallenge
	{
		Guid LadderChallengeId { get; }
		Guid LadderId { get; }

		ILadderTeam ChallengerLadderTeam { get; }
		ILadderTeam ChallengedLadderTeam { get; }

		DateTimeOffset ChallengedAtTime { get; }

		LadderChallengeState ChallengeState { get; }
		string MatchResults { get; }
		DateTimeOffset? MatchResultsReportedTime { get; }
	}

	internal class LadderChallenge : ILadderChallenge
	{
		public Guid LadderChallengeId { get; set; }
		public Guid LadderId { get; set; }

		public ILadderTeam ChallengerLadderTeam { get; set; }
		public ILadderTeam ChallengedLadderTeam { get; set; }

		public DateTimeOffset ChallengedAtTime { get; set; }

		public LadderChallengeState ChallengeState { get; set; }
		public string MatchResults { get; set; }
		public DateTimeOffset? MatchResultsReportedTime { get; set; }

		public override bool Equals(object other)
		{
			return other is LadderChallenge otherLadderChallenge && LadderChallengeId.Equals(otherLadderChallenge.LadderChallengeId);
		}

		public override int GetHashCode()
		{
			return LadderChallengeId.GetHashCode();
		}
	}
}
