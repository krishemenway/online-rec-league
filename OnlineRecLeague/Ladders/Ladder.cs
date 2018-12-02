using System;
using System.Collections.Generic;
using OnlineRecLeague.Ladders.LadderRules;
using OnlineRecLeague.Sports;

namespace OnlineRecLeague.Ladders
{
	public interface ILadder
	{
		Guid LadderId { get; }

		Guid SportId { get; }
		ISport Sport { get; }

		string Name { get; }
		string UriPath { get; }

		IRuleset Rules { get; }

		IReadOnlyList<ILadderTeam> AllLadderTeams { get; }
		IReadOnlyList<ILadderChallenge> AllLadderChallenges { get; }
	}

	public class Ladder : ILadder
	{
		public Ladder()
		{
			_lazyAllLadderChallenges = new Lazy<IReadOnlyList<ILadderChallenge>>(() => new LadderChallengeStore().FindAll(this));
			_lazyAllLadderTeams = new Lazy<IReadOnlyList<ILadderTeam>>(() => new LadderTeamStore().Find(this));
			_lazySport = new Lazy<ISport>(() => new SportStore().FindById(SportId)[SportId]);
		}

		public Guid LadderId { get; set; }
		public string Name { get; set; }
		public string UriPath { get; set; }

		public Guid SportId { get; set; }
		public ISport Sport => _lazySport.Value;

		public IReadOnlyList<ILadderTeam> AllLadderTeams => _lazyAllLadderTeams.Value;
		public IReadOnlyList<ILadderChallenge> AllLadderChallenges => _lazyAllLadderChallenges.Value;

		public IRuleset Rules { get; set; }

		public override bool Equals(object other)
		{
			return other is Ladder otherLadder && LadderId.Equals(otherLadder.LadderId);
		}

		public override int GetHashCode()
		{
			return LadderId.GetHashCode();
		}

		private readonly Lazy<IReadOnlyList<ILadderChallenge>> _lazyAllLadderChallenges;
		private readonly Lazy<IReadOnlyList<ILadderTeam>> _lazyAllLadderTeams;
		private readonly Lazy<ISport> _lazySport;
	}
}
