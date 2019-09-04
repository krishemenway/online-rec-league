using System;
using OnlineRecLeague.Teams;

namespace OnlineRecLeague.LeagueTeams
{
	public interface ILeagueTeam
	{
		Guid LeagueTeamId { get; }

		Guid TeamId { get; }
		ITeam Team { get; }

		string Name { get; }

		DateTimeOffset CreatedAtTime { get; }
		Guid CreatedByUserId { get; }
	}

	public class LeagueTeam : ILeagueTeam
	{
		public LeagueTeam(Guid leagueTeamId)
		{
			LeagueTeamId = leagueTeamId;
		}

		public Guid LeagueTeamId { get; }

		public Guid TeamId { get; set; }
		public ITeam Team { get; set; }

		public string Name { get; set; }

		public DateTimeOffset CreatedAtTime { get; set; }
		public Guid CreatedByUserId { get; set; }

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
