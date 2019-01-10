using System;

namespace OnlineRecLeague.LadderChallenges
{
	public class LadderChallengeRecord
	{
		public Guid LadderChallengeId { get; set; }
		public Guid LadderId { get; set; }

		public Guid ChallengerLadderTeamId { get; set; }
		public Guid ChallengedLadderTeamId { get; set; }

		public DateTimeOffset ChallengedTime { get; set; }
		public DateTimeOffset? MatchTime { get; set; }

		public bool ChallengeSuccessful { get; set; }

		public string MatchResults { get; set; }
		public DateTimeOffset? MatchResultsReportedTime { get; set; }
	}
}
