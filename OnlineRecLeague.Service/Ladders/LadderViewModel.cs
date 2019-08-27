using OnlineRecLeague.LadderTeams;
using System;
using System.Collections.Generic;

namespace OnlineRecLeague.Ladders
{
	public class LadderViewModel
	{
		public Guid LadderId { get; set; }
		public string Name { get; set; }

		public IReadOnlyList<LadderTeamViewModel> AllTeams { get; set; }
	}
}
