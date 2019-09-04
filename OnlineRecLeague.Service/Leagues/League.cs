using System;
using OnlineRecLeague.Rulesets;
using OnlineRecLeague.Service.LeagueMatchStrategies;
using OnlineRecLeague.Games;

namespace OnlineRecLeague.Leagues
{
	public interface ILeague
	{
		Guid LeagueId { get; }

		Guid GameId { get; }
		IGame Game { get; }

		string Name { get; }
		string UriPath { get; }

		IRuleset Rules { get; }

		LeagueMatchStrategies LeagueMatchStrategy { get; }
	}

	internal class League : ILeague
	{
		internal League(LeagueRecord record)
		{
			LeagueId = record.LeagueId;
			Name = record.Name;
			UriPath = record.UriPath;
			GameId = record.GameId;

			_lazyGame = new Lazy<IGame>(() => new GameStore().FindGamesByGameId(GameId)[GameId]);
			_lazyRules = new Lazy<IRuleset>(() => new RulesetFactory().Create(record.Rules));
		}

		public Guid LeagueId { get; }
		public string Name { get; }
		public string UriPath { get; }
		
		public LeagueMatchStrategies LeagueMatchStrategy { get; set; }

		public Guid GameId { get; }
		public IGame Game => _lazyGame.Value;

		public IRuleset Rules { get; set; }

		public override bool Equals(object other)
		{
			return other is League otherLeague && LeagueId.Equals(otherLeague.LeagueId);
		}

		public override int GetHashCode()
		{
			return LeagueId.GetHashCode();
		}

		private readonly Lazy<IGame> _lazyGame;
		private readonly Lazy<IRuleset> _lazyRules;
	}
}
