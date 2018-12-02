using OnlineRecLeague.Ladders;
using System.Collections.Generic;

namespace OnlineRecLeague.Games
{
	public class GameProfile
	{
		public IReadOnlyList<LadderViewModel> Ladders { get; set; }
	}
}
