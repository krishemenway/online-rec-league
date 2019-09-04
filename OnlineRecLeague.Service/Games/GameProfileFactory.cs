using OnlineRecLeague.Leagues;
using System.Linq;

namespace OnlineRecLeague.Games
{
	public interface IGameProfileFactory
	{
		GameProfile Create(IGame game);
	}

	public class GameProfileFactory : IGameProfileFactory
	{
		public GameProfileFactory(ILeagueViewModelFactory leagueViewModelFactory = null)
		{
			_leagueViewModelFactory = leagueViewModelFactory ?? new LeagueViewModelFactory();
		}

		public GameProfile Create(IGame game)
		{
			return new GameProfile
				{
					Leagues = game.Leagues.Select(league => _leagueViewModelFactory.CreateBriefViewModel(league)).ToList(),
				};
		}

		private readonly ILeagueViewModelFactory _leagueViewModelFactory;
	}
}
