using System;
using System.Collections.Generic;

namespace LeagueService.Ladders
{
	public interface ILadder
	{
		Guid LadderId { get; }
		Guid EsportId { get; }

		string Name { get; }
		string UriPath { get; }

		bool ForcesRealNames { get; }

		IReadOnlyList<ILadderTeam> AllLadderTeams { get; }
		IReadOnlyList<ILadderChallenge> AllLadderChallenges { get; }
	}

	public class Ladder : ILadder
	{
		public Ladder()
		{
			_lazyAllLadderChallenges = new Lazy<IReadOnlyList<ILadderChallenge>>(() => new LadderChallengeStore().FindAll(this));
			_lazyAllLadderTeams = new Lazy<IReadOnlyList<ILadderTeam>>(() => new LadderTeamStore().FindAllTeams(this));
		}

		public Guid LadderId { get; set; }
		public Guid EsportId { get; set; }

		public string Name { get; set; }
		public string UriPath { get; set; }

		public bool ForcesRealNames { get; set; }

		public IReadOnlyList<ILadderTeam> AllLadderTeams => _lazyAllLadderTeams.Value;
		public IReadOnlyList<ILadderChallenge> AllLadderChallenges => _lazyAllLadderChallenges.Value;

		public override bool Equals(object obj)
		{
			var objAsLadder = obj as ILadder;
			return objAsLadder != null && LadderId.Equals(objAsLadder.LadderId);
		}

		public override int GetHashCode()
		{
			return LadderId.GetHashCode();
		}

		private readonly Lazy<IReadOnlyList<ILadderChallenge>> _lazyAllLadderChallenges;
		private readonly Lazy<IReadOnlyList<ILadderTeam>> _lazyAllLadderTeams;
	}
}
