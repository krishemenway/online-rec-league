using System;
using System.Collections.Generic;

namespace OnlineRecLeague.TeamMembers
{
	public class InviteToTeamRequest
	{
		public Guid TeamId { get; set; }
		public IReadOnlyList<string> Emails { get; set; }
	}
}
