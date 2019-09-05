using OnlineRecLeague.Service.DataTypes;
using OnlineRecLeague.Teams;

namespace OnlineRecLeague.LeagueTeams
{
	public interface ILeagueTeamViewModelFactory
	{
		LeagueTeamViewModel Create(ILeagueTeam leagueTeam);
	}

	public class LeagueTeamViewModelFactory : ILeagueTeamViewModelFactory
	{
		public LeagueTeamViewModel Create(ILeagueTeam leagueTeam)
		{
			return new LeagueTeamViewModel
				{
					TeamId = leagueTeam.Team.TeamId,
					Name = leagueTeam.Team.Name,
				};
		}
	}

	public class LeagueTeamViewModel
	{
		public Id<Team> TeamId { get; set; }
		public string Name { get; set; }
	}
}
