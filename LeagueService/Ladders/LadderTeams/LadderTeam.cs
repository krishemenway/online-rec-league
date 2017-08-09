using System;
using LeagueService.Teams;

namespace LeagueService.Ladders
{
	public interface ILadderTeam
	{
		Guid LadderTeamId { get; }
		ITeam Team { get; }

		int Ranking { get; }

		DateTime CreatedAtTime { get; }
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

		public int Ranking { get; set; }

		public DateTime CreatedAtTime { get; set; }
		public Guid CreatedByUserId { get; set; }

		public override bool Equals(object obj)
		{
			var objAsLadderTeam = obj as ILadderTeam;
			return objAsLadderTeam != null && LadderTeamId.Equals(objAsLadderTeam.LadderTeamId);
		}

		public override int GetHashCode()
		{
			return LadderTeamId.GetHashCode();
		}
	}
}
