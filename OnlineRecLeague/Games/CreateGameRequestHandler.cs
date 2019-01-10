using OnlineRecLeague.DataTypes;

namespace OnlineRecLeague.Games
{
	public interface ICreateGameRequestHandler
	{
		Result<GameProfile> HandleRequest(CreateGameRequest request);
	}

	public class CreateGameRequestHandler : ICreateGameRequestHandler
	{
		public CreateGameRequestHandler(
			IGameStore gameStore = null,
			IGameProfileFactory gameProfileFactory = null)
		{
			_gameStore = gameStore ?? new GameStore();
			_gameProfileFactory = gameProfileFactory ?? new GameProfileFactory();
		}

		public Result<GameProfile> HandleRequest(CreateGameRequest request)
		{
			var game = _gameStore.Save(request);
			var gameProfile = _gameProfileFactory.Create(game);

			return Result<GameProfile>.Successful(gameProfile);
		}

		private readonly IGameStore _gameStore;
		private readonly IGameProfileFactory _gameProfileFactory;
	}
}
