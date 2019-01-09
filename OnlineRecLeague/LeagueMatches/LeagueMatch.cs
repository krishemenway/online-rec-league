using System;
using System.Collections.Generic;

namespace OnlineRecLeague.LeagueMatches
{
	public interface ILeagueMatch
	{
		DateTimeOffset CreatedTime { get; }
		DateTimeOffset MatchStartTime { get; }

		IReadOnlyList<Guid> TeamIds { get; }
	}

	public class LeagueMatch : ILeagueMatch
	{
		public DateTimeOffset CreatedTime { get; set; }
		public DateTimeOffset MatchStartTime { get; set; }

		public IReadOnlyList<Guid> TeamIds { get; set; }
	}
}
