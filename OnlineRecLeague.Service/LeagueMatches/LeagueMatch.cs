using OnlineRecLeague.Leagues;
using OnlineRecLeague.LeagueTeams;
using OnlineRecLeague.Service.DataTypes;

namespace OnlineRecLeague.LeagueMatches
{
	public interface ILeagueMatch
	{
		Id<LeagueMatch> LeagueMatchId { get; }
		Id<League> LeagueId { get; }

		Id<LeagueTeam> HomeLeagueTeamId { get; }
		ILeagueTeam HomeTeam { get; }

		Id<LeagueTeam> AwayLeagueTeamId { get; }
		ILeagueTeam AwayTeam { get; }

		ILeagueTeam WinningTeam { get; }
	}

	public class LeagueMatch : ILeagueMatch
	{
		public Id<LeagueMatch> LeagueMatchId { get; set; }
		public Id<League> LeagueId { get; set; }

		public Id<LeagueTeam> HomeLeagueTeamId { get; set; }
		public ILeagueTeam HomeTeam { get; set; }

		public Id<LeagueTeam> AwayLeagueTeamId { get; set; }
		public ILeagueTeam AwayTeam { get; set; }

		public ILeagueTeam WinningTeam { get; set; }

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
