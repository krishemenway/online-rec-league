using OnlineRecLeague.Users;
using Microsoft.AspNetCore.Mvc;

namespace OnlineRecLeague.Ladders
{
	[Route("api/ladders")]
	public class LadderController : Controller
	{
		[HttpGet("all")]
		public IActionResult All()
		{
			return Json(new FindAllLaddersRequestHandler().HandleRequest());
		}

		[HttpGet("find")]
		public IActionResult Find([FromQuery]FindLadderRequest request)
		{
			return Json(new FindLadderRequestHandler().HandleRequest(request));
		}

		[RequiresLoggedInUser]
		[HttpPost("create")]
		public IActionResult Create([FromBody]CreateLadderRequest request)
		{
			return Json(new CreateLadderRequestHandler().HandleRequest(request));
		}
	}
}
