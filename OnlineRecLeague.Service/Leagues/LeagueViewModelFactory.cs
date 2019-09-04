using OnlineRecLeague.LeagueTeams;

namespace OnlineRecLeague.Leagues
{
	public interface ILeagueViewModelFactory
	{
		LeagueViewModel CreateBriefViewModel(ILeague league);
		LeagueViewModel CreateDetailedViewModel(ILeague league);
	}

	internal class LeagueViewModelFactory : ILeagueViewModelFactory
	{
		public LeagueViewModel CreateBriefViewModel(ILeague league)
		{
			return new LeagueViewModel
				{
					LeagueId = league.LeagueId,
					Name = league.Name,
				};
		}

		public LeagueViewModel CreateDetailedViewModel(ILeague league)
		{
			return new LeagueViewModel
				{
					LeagueId = league.LeagueId,
					Name = league.Name,
				};
		}
	}
}
