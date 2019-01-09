using System;

namespace OnlineRecLeague.Leagues
{
	public interface ILeagueStore
	{
		ILeague FindById(params Guid[] leagueId);
		ILeague Create(CreateLeagueRequest request);
	}

	public class LeagueStore : ILeagueStore
	{
		public ILeague FindById(params Guid[] leagueId)
		{
			throw new NotImplementedException();
		}

		public ILeague Create(CreateLeagueRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
