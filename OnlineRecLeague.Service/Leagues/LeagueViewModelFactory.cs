﻿using OnlineRecLeague.Service.DataTypes;

namespace OnlineRecLeague.Leagues
{
	public interface ILeagueViewModelFactory
	{
		LeagueViewModel CreateBriefViewModel(ILeague league);
		LeagueViewModel CreateDetailedViewModel(ILeague league);
	}

	public class LeagueViewModelFactory : ILeagueViewModelFactory
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

	public class LeagueViewModel
	{
		public Id<League> LeagueId { get; set; }
		public string Name { get; set; }
	}
}
