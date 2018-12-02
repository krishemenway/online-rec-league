using System;

namespace OnlineRecLeague.Ladders
{
	public class LadderChallengeRequest
	{
		public Guid LadderId { get; set; }
		public Guid ChallengerLadderTeamId { get; set; }
		public Guid ChallengedLadderTeamId { get; set; }
		public DateTimeOffset ChallengedTime { get; set; }
	}
}
