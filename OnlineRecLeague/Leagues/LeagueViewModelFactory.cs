using OnlineRecLeague.Users;

namespace OnlineRecLeague.Leagues
{
	public interface ILeagueViewModelFactory
	{
		LeagueViewModel Create(ILeague league, IUser userInSession);
	}

	internal class LeagueViewModelFactory : ILeagueViewModelFactory
	{
		public LeagueViewModel Create(ILeague league, IUser userInSession)
		{
			return new LeagueViewModel();
		}
	}
}
