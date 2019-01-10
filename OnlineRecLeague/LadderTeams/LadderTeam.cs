using System;
using OnlineRecLeague.Teams;

namespace OnlineRecLeague.LadderTeams
{
	public interface ILadderTeam
	{
		Guid LadderTeamId { get; }
		ITeam Team { get; }

		int Rung { get; }

		DateTimeOffset CreatedAtTime { get; }
		Guid CreatedByUserId { get; }
	}

	public class LadderTeam : ILadderTeam
	{
		public LadderTeam(Guid ladderTeamId)
		{
			LadderTeamId = ladderTeamId;
		}

		public Guid LadderTeamId { get; }

		public ITeam Team { get; set; }

		public int Rung { get; set; }

		public DateTimeOffset CreatedAtTime { get; set; }
		public Guid CreatedByUserId { get; set; }

		public override bool Equals(object other)
		{
			return other is ILadderTeam otherLadderTeam && LadderTeamId.Equals(otherLadderTeam.LadderTeamId);
		}

		public override int GetHashCode()
		{
			return LadderTeamId.GetHashCode();
		}
	}
}
