using OnlineRecLeague.Rulesets;
using System;

namespace OnlineRecLeague.Leagues
{
	public interface ILeague
	{
		Guid LeagueId { get; }
	}

	public class League : ILeague
	{
		internal League(LeagueRecord record)
		{
			LeagueId = record.LeagueId;
			_lazyRuleSet = new Lazy<IRuleset>(() => new RulesetFactory().Create(record.Rules));
		}

		public Guid LeagueId { get; }
		public IRuleset Ruleset => _lazyRuleSet.Value;

		public override bool Equals(object other)
		{
			return other is League otherLeague && LeagueId.Equals(otherLeague.LeagueId);
		}

		public override int GetHashCode()
		{
			return LeagueId.GetHashCode();
		}

		private readonly Lazy<IRuleset> _lazyRuleSet;
	}
}
