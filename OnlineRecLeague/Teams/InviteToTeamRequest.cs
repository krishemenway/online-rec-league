using System;
using System.Collections.Generic;

namespace OnlineRecLeague.Teams
{
	public class InviteToTeamRequest
	{
		public Guid TeamId { get; set; }
		public IReadOnlyList<string> Emails { get; set; }
	}
}
