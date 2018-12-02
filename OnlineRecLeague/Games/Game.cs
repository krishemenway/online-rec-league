using OnlineRecLeague.Ladders;
using System;
using System.Collections.Generic;

namespace OnlineRecLeague.Games
{
	public interface IGame
	{
		Guid GameId { get; }
		string Name { get; }

		DateTimeOffset ReleaseDate { get; }

		string IconImagePath { get; }
		string BackgroundImagePath { get; }

		IReadOnlyList<ILadder> Ladders { get; }
	}

	public class Game : IGame
	{
		public Game(Func<IReadOnlyList<ILadder>> findAllLaddersForGameFunc = null)
		{
			_lazyLadders = new Lazy<IReadOnlyList<ILadder>>(findAllLaddersForGameFunc ?? (() => new LadderStore().FindAll(this)));
		}

		public Guid GameId { get; set; }
		public string Name { get; set; }

		public DateTimeOffset ReleaseDate { get; set; }

		public string IconImagePath { get; set; }
		public string BackgroundImagePath { get; set; }

		public IReadOnlyList<ILadder> Ladders => _lazyLadders.Value;

		public override bool Equals(object other)
		{
			return other is Game otherGame && GameId.Equals(otherGame.GameId);
		}

		public override int GetHashCode()
		{
			return GameId.GetHashCode();
		}

		private readonly Lazy<IReadOnlyList<ILadder>> _lazyLadders;
	}
}
