using System;
using System.Collections.Generic;
using OnlineRecLeague.LadderChallenges;
using OnlineRecLeague.LadderTeams;
using OnlineRecLeague.Rulesets;
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

	internal class Ladder : ILadder
	{
		internal Ladder(LadderRecord record)
		{
			LadderId = record.LadderId;
			Name = record.Name;
			UriPath = record.UriPath;
			SportId = record.SportId;

			_lazyAllLadderChallenges = new Lazy<IReadOnlyList<ILadderChallenge>>(() => new LadderChallengeStore().FindAll(this));
			_lazyAllLadderTeams = new Lazy<IReadOnlyList<ILadderTeam>>(() => new LadderTeamStore().Find(this));
			_lazySport = new Lazy<ISport>(() => new SportStore().FindById(SportId)[SportId]);
			_lazyRules = new Lazy<IRuleset>(() => new RulesetFactory().Create(record.Rules));
		}

		public Guid LadderId { get; }
		public string Name { get; }
		public string UriPath { get; }

		public Guid SportId { get; }
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
		private readonly Lazy<IRuleset> _lazyRules;
	}
}
