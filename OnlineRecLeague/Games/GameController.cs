using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRecLeague.Games
{
	[Route("api/games")]
	public class GameController : Controller
	{
		[HttpPost("create")]
		public IActionResult Create()
		{
			return Json(new object());
		}

		[HttpGet("thing")]
		public IActionResult Thing()
		{
			return Json(new object());
		}
	}
}
