using System;

namespace LeagueService.Games
{
	public interface IGame
	{
		Guid GameId { get; }
		string Name { get; }
	}

	public class Game : IGame
	{
		public Guid GameId { get; set; }
		public string Name { get; set; }
	}
}
