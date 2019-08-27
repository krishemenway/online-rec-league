using OnlineRecLeague.Ladders;
using System.Linq;

namespace OnlineRecLeague.Games
{
	public interface IGameProfileFactory
	{
		GameProfile Create(IGame game);
	}

	public class GameProfileFactory : IGameProfileFactory
	{
		public GameProfileFactory(ILadderViewModelFactory ladderViewModelFactory = null)
		{
			_ladderViewModelFactory = ladderViewModelFactory ?? new LadderViewModelFactory();
		}

		public GameProfile Create(IGame game)
		{
			return new GameProfile
				{
					Ladders = game.Ladders.Select(ladder => _ladderViewModelFactory.CreateBriefViewModel(ladder)).ToList(),
				};
		}

		private readonly ILadderViewModelFactory _ladderViewModelFactory;
	}
}
