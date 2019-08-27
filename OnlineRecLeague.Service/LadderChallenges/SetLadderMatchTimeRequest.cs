using System;

namespace OnlineRecLeague.LadderChallenges
{
	public class SetLadderMatchTimeRequest
	{
		public Guid LadderChallengeId { get; set; }
		public DateTimeOffset MatchTime { get; set; }
	}
}
