using Microsoft.AspNetCore.Mvc;
using System;

namespace LeagueService.Ladders
{
	public class LadderController : Controller
	{
		[HttpGet("all")]
		public IActionResult All()
		{
			return Json(new LadderRepository().FindAll());
		}

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
	}
}
