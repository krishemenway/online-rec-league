using System;

namespace OnlineRecLeague.LadderChallenges
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
