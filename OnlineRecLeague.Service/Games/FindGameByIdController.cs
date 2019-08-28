using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;
using System;

namespace OnlineRecLeague.Games
{
	[ApiController]
	[Route("api/games")]
	[RequiresAdminAccess]
	public class FindGameByIdController : ControllerBase
	{
		[HttpGet(nameof(FindById))]
		[ProducesResponseType(200, Type = typeof(Result<GameProfile>))]
		public ActionResult<Result<GameProfile>> FindById([FromQuery] FindGameByIdRequest request)
		{
			throw new NotImplementedException();
		}
	}
}