using System;

namespace OnlineRecLeague.Leagues
{
	public interface ILeagueStore
	{
		ILeague FindById(params Guid[] leagueId);
	}

	public class LeagueStore : ILeagueStore
	{
		public ILeague FindById(params Guid[] leagueId)
		{
			throw new NotImplementedException();
		}
	}
}
