using System;

namespace OnlineRecLeague.Ladders.LadderChallenges
{
	public enum LadderChallengeState
	{
		Challenged,

		WaitingForMatch,
		WaitingForResults,

		Failed,
		Succeeded,
	}
}
