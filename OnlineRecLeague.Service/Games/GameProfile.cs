using OnlineRecLeague.Leagues;
using System.Collections.Generic;

namespace OnlineRecLeague.Games
{
	public class GameProfile
	{
		public IReadOnlyList<LeagueViewModel> Leagues { get; set; }
	}
}
