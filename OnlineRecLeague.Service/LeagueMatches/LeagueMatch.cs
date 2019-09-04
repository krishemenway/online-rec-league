using OnlineRecLeague.LeagueTeams;
using System;

namespace OnlineRecLeague.LeagueMatches
{
	public interface ILeagueMatch
	{
		Guid LeagueMatchId { get; }

		Guid LeagueId { get; }

		Guid HomeLeagueTeamId { get; }
		Guid AwayLeagueTeamId { get; }
	}

	internal class LeagueMatch : ILeagueMatch
	{
		public Guid LeagueMatchId { get; set; }
		public Guid LeagueId { get; set; }

		public Guid HomeLeagueTeamId { get; set; }
		public Guid AwayLeagueTeamId { get; set; }

		public override bool Equals(object other)
		{
			return other is LeagueMatch otherLeagueMatch && LeagueMatchId.Equals(otherLeagueMatch.LeagueMatchId);
		}

		public override int GetHashCode()
		{
			return LeagueMatchId.GetHashCode();
		}
	}
}
