using System.Collections.Generic;

namespace OnlineRecLeague.Games
{
	public class FindGameByNameResponse
	{
		public IReadOnlyList<GameProfile> FoundGames { get; set; }
	}
}