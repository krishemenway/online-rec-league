using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;
using System;

namespace OnlineRecLeague.Games
{
	[ApiController]
	[Route("api/games")]
	[RequiresAdminAccess]
	public class CreateGameController : ControllerBase
	{
		public CreateGameController(
			IGameStore gameStore = null,
			IGameProfileFactory gameProfileFactory = null)
		{
			_gameStore = gameStore ?? new GameStore();
			_gameProfileFactory = gameProfileFactory ?? new GameProfileFactory();
		}

		[HttpPost(nameof(Create))]
		[ProducesResponseType(200, Type = typeof(Result<GameProfile>))]
		public ActionResult<Result<GameProfile>> Create([FromBody] CreateGameRequest request)
		{
			var game = _gameStore.Save(request);
			var gameProfile = _gameProfileFactory.Create(game);

			return Result<GameProfile>.Successful(gameProfile);
		}

		private readonly IGameStore _gameStore;
		private readonly IGameProfileFactory _gameProfileFactory;
	}

	public class CreateGameRequest
	{
		public string Name { get; set; }
		public DateTimeOffset ReleaseDate { get; set; }
	}
}
