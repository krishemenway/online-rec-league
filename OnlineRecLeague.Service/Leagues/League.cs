using System;
using OnlineRecLeague.Rulesets;
using OnlineRecLeague.Service.LeagueMatchStrategies;
using OnlineRecLeague.Games;
using OnlineRecLeague.Service.DataTypes;

namespace OnlineRecLeague.Leagues
{
	public interface ILeague
	{
		Id<League> LeagueId { get; }

		Id<Game> GameId { get; }
		IGame Game { get; }

		string Name { get; }
		string UriPath { get; }

		IRuleset Rules { get; }

		LeagueMatchStrategies LeagueMatchStrategy { get; }
	}

	public class League : ILeague
	{
		public League(LeagueRecord record)
		{
			LeagueId = record.LeagueId;
			Name = record.Name;
			UriPath = record.UriPath;
			GameId = record.GameId;
			RulesJson = record.Rules;

			_lazyGame = new Lazy<IGame>(() => new GameStore().FindGamesByGameId(GameId)[GameId]);
			_lazyRules = new Lazy<IRuleset>(() => new RulesetFactory().Create(record.Rules));
		}

		public Id<League> LeagueId { get; }
		public string Name { get; }
		public string UriPath { get; }
		
		public LeagueMatchStrategies LeagueMatchStrategy { get; set; }

		public IGame Game => _lazyGame.Value;
		public Id<Game> GameId { get; }

		public IRuleset Rules => _lazyRules.Value;
		public string RulesJson { get; }

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
