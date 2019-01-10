namespace OnlineRecLeague.LadderChallenges
{
	public interface ILadderChallengeStateAnalyzer
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

			if (record.MatchTime.HasValue)
			{
				return LadderChallengeState.WaitingForMatch;
			}

			return LadderChallengeState.Challenged;
		}
	}
}
