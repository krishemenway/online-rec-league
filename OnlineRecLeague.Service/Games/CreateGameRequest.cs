using System;

namespace OnlineRecLeague.Games
{
	public class CreateGameRequest
	{
		public string Name { get; set; }
		public DateTimeOffset ReleaseDate { get; set; }
	}
}
