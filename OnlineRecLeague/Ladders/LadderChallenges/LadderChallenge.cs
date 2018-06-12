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

		DateTime ChallengedAtTime { get; }

		LadderChallengeState ChallengeState { get; }
		string MatchResults { get; }
		DateTime? MatchResultsReportedTime { get; }
	}

	public class LadderChallenge : ILadderChallenge
	{
		public Guid LadderChallengeId { get; set; }
		public Guid LadderId { get; set; }

		public ILadderTeam ChallengerLadderTeam { get; set; }
		public ILadderTeam ChallengedLadderTeam { get; set; }

		public DateTime ChallengedAtTime { get; set; }

		public LadderChallengeState ChallengeState { get; set; }
		public string MatchResults { get; set; }
		public DateTime? MatchResultsReportedTime { get; set; }

		public override bool Equals(object obj)
		{
			var objAsLadderChallenge = obj as ILadderChallenge;
			return objAsLadderChallenge != null && LadderChallengeId.Equals(objAsLadderChallenge.LadderChallengeId);
		}

		public override int GetHashCode()
		{
			return LadderChallengeId.GetHashCode();
		}
	}
}
