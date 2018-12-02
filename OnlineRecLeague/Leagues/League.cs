using System;

namespace OnlineRecLeague.Leagues
{
	public interface ILeague
	{
		Guid LeagueId { get; }
	}

	public class League : ILeague
	{
		public Guid LeagueId { get; set; }

		public override bool Equals(object other)
		{
			return other is League otherLeague && LeagueId.Equals(otherLeague.LeagueId);
		}

		public override int GetHashCode()
		{
			return LeagueId.GetHashCode();
		}
	}
}
