using System;
using OnlineRecLeague.Service.DataTypes;
using OnlineRecLeague.Teams;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.LeagueTeams
{
	public interface ILeagueTeam
	{
		Id<LeagueTeam> LeagueTeamId { get; }

		Id<Team> TeamId { get; }
		ITeam Team { get; }

		string Name { get; }

		DateTimeOffset CreatedAtTime { get; }
		Id<User> CreatedByUserId { get; }
	}

	public class LeagueTeam : ILeagueTeam
	{
		public LeagueTeam(Id<LeagueTeam> leagueTeamId)
		{
			LeagueTeamId = leagueTeamId;
		}

		public Id<LeagueTeam> LeagueTeamId { get; }

		public Id<Team> TeamId { get; set; }
		public ITeam Team { get; set; }

		public string Name { get; set; }

		public DateTimeOffset CreatedAtTime { get; set; }
		public Id<User> CreatedByUserId { get; set; }

		public override bool Equals(object other)
		{
			return other is ILeagueTeam otherLeagueTeam && LeagueTeamId.Equals(otherLeagueTeam.LeagueTeamId);
		}

		public override int GetHashCode()
		{
			return LeagueTeamId.GetHashCode();
		}
	}
}
