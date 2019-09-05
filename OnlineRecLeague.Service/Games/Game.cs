using OnlineRecLeague.Leagues;
using OnlineRecLeague.Service.DataTypes;
using System;
using System.Collections.Generic;

namespace OnlineRecLeague.Games
{
	public interface IGame
	{
		Id<Game> GameId { get; }
		string Name { get; }

		DateTimeOffset ReleaseDate { get; }

		string IconImagePath { get; }
		string BackgroundImagePath { get; }

		IReadOnlyList<ILeague> Leagues { get; }
	}

	public class Game : IGame
	{
		public Game(Func<IReadOnlyList<ILeague>> findAllLeaguesForGameFunc = null)
		{
			_lazyLeagues = new Lazy<IReadOnlyList<ILeague>>(findAllLeaguesForGameFunc ?? (() => new LeagueStore().FindAll(this)));
		}

		public Id<Game> GameId { get; set; }
		public string Name { get; set; }

		public DateTimeOffset ReleaseDate { get; set; }

		public string IconImagePath { get; set; }
		public string BackgroundImagePath { get; set; }

		public IReadOnlyList<ILeague> Leagues => _lazyLeagues.Value;

		public override bool Equals(object other)
		{
			return other is Game otherGame && GameId.Equals(otherGame.GameId);
		}

		public override int GetHashCode()
		{
			return GameId.GetHashCode();
		}

		private readonly Lazy<IReadOnlyList<ILeague>> _lazyLeagues;
	}
}
