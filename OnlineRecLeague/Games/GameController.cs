﻿using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.CommonDataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Games
{
	[Route("api/games")]
	[RequiresSuperadminAccess]
	public class GameController : Controller
	{
		[HttpPost("Create")]
		[RequiresSuperadminAccess]
		[ProducesResponseType(200, Type = typeof(Result<GameProfile>))]
		public IActionResult Create([FromBody]CreateGameRequest request)
		{
			return Json(new CreateGameRequestHandler().HandleRequest(request));
		}

		[HttpGet("FindById")]
		[ProducesResponseType(200, Type = typeof(Result<GameProfile>))]
		public IActionResult FindById([FromQuery]FindGameByIdRequest request)
		{
			return Json(new FindGameByIdRequestHandler().HandleRequest(request));
		}

		[HttpGet("FindByName")]
		[ProducesResponseType(200, Type = typeof(Result<FindGameByNameResponse>))]
		public IActionResult FindByName([FromQuery]FindGameByNameRequest request)
		{
			return Json(new FindGameByNameRequestHandler().HandleRequest(request));
		}
	}
}