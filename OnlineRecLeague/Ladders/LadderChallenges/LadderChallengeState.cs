using System;

namespace OnlineRecLeague.Ladders.LadderChallenges
{
	public enum LadderChallengeState
	{
		Challenged,
		WaitingForResults,

		Failed,
		Succeeded,
		NotReported
	}

	internal interface ILadderChallengeStateAnalyzer
	{
		LadderChallengeState Analyze(LadderChallengeRecord record);
	}

	internal class LadderChallengeStateAnalyzer : ILadderChallengeStateAnalyzer
	{
		public LadderChallengeState Analyze(LadderChallengeRecord record)
		{
			if (!record.MatchResultsReportedTime.HasValue)
			{
				if (record.ChallengeSuccessful)
				{
					return LadderChallengeState.Succeeded;
				}
				else
				{
					return LadderChallengeState.Failed;
				}
			}

			return LadderChallengeState.Challenged;
		}
	}

	public class UnknownChallengeStateException : Exception
	{
		public UnknownChallengeStateException(Guid ladderChallengeId) : base($"Unknown challenge state for challenge {ladderChallengeId}") { }
	}
}
