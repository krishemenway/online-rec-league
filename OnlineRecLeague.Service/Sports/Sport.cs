using OnlineRecLeague.Games;
using System;

namespace OnlineRecLeague.Sports
{
	public interface ISport
	{
		Guid SportId { get; }
		string Name { get; }

		IGame Game { get; }
	}

	public class Sport : ISport
	{
		public Sport(Guid sportId, Func<IGame> findGameForSportFunc = null)
		{
			SportId = sportId;
			_lazyGame = new Lazy<IGame>(() => ((findGameForSportFunc) ?? (() => new GameStore().FindGamesByGameId(new[] { GameId })[GameId])).Invoke());
		}

		public Guid SportId { get; set; }
		public string Name { get; set; }

		public Guid GameId { get; set; }
		public IGame Game { get; set; }

		public override bool Equals(object other)
		{
			return other is Sport otherSport && SportId.Equals(otherSport.SportId);
		}

		public override int GetHashCode()
		{
			return SportId.GetHashCode();
		}

		private readonly Lazy<IGame> _lazyGame;
	}
}
