using System;
using System.Collections.Generic;

namespace OnlineRecLeague.LeagueMatches
{
	public interface ILeagueMatchStore
	{
		IReadOnlyList<ILeagueMatch> FindMatches(Guid leagueId);
	}

	public class LeagueMatchStore : ILeagueMatchStore
	{
		public IReadOnlyList<ILeagueMatch> FindMatches(Guid teamId)
		{
			throw new NotImplementedException();
		}
	}
}
