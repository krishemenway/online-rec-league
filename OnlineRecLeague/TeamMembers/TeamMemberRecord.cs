using System;

namespace OnlineRecLeague.TeamMembers
{
	public class TeamMemberRecord
	{
		public Guid TeamMemberId { get; set; }

		public Guid TeamId { get; set; }
		public Guid UserId { get; set; }

		public string NickName { get; set; }
		public DateTime JoinedTime { get; set; }
	}
}
