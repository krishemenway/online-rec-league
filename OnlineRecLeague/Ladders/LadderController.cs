using OnlineRecLeague.Users;
using Microsoft.AspNetCore.Mvc;
using System;

namespace OnlineRecLeague.Ladders
{
	[Route("api/ladders")]
	public class LadderController : Controller
	{
		[HttpGet("all")]
		public IActionResult All()
		{
			return Json(new LadderRepository().FindAll());
		}

		[RequiresLoggedInUser]
		[HttpPost("create")]
		public IActionResult Create(CreateLadderRequest request)
		{
			return Json(new LadderRepository().Create(request));
		}
	}

	public class CreateLadderRequest
	{
		public string Name { get; set; }
		public string UriPath { get; set; }
		public Guid EsportId { get; set; }
		public string Rules { get; set; }
	}
}
