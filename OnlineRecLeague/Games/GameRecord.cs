using System;

namespace OnlineRecLeague.Games
{
	public class GameRecord
	{
		public Guid GameId { get; set; }
		public string Name { get; set; }
		public DateTimeOffset ReleaseDate { get; set; }
	}
}
