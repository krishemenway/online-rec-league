using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;
using System;

namespace OnlineRecLeague.Games
{
	[ApiController]
	[Route("api/games")]
	[RequiresAdminAccess]
	public class FindGameByNameController : ControllerBase
	{
		[HttpGet(nameof(FindByName))]
		[ProducesResponseType(200, Type = typeof(Result<FindGameByNameResponse>))]
		public ActionResult<Result<FindGameByNameResponse>> FindByName([FromQuery] FindGameByNameRequest request)
		{
			throw new NotImplementedException();
		}
	}
}