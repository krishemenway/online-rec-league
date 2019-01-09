using OnlineRecLeague.Users;
using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.CommonDataTypes;
using System.Collections.Generic;

namespace OnlineRecLeague.Ladders
{
	[Route("api/ladders")]
	public class LadderController : Controller
	{
		[HttpGet("all")]
		[ProducesResponseType(200, Type = typeof(Result<IReadOnlyList<LadderViewModel>>))]
		public IActionResult All()
		{
			return Json(new FindAllLaddersRequestHandler().HandleRequest());
		}

		[HttpGet("find")]
		[ProducesResponseType(200, Type = typeof(Result<LadderViewModel>))]
		public IActionResult Find([FromQuery]FindLadderRequest request)
		{
			return Json(new FindLadderRequestHandler().HandleRequest(request));
		}

		[HttpPost("create")]
		[RequiresUserInSession]
		[ProducesResponseType(200, Type = typeof(Result<LadderViewModel>))]
		public IActionResult Create([FromBody]CreateLadderRequest request)
		{
			return Json(new CreateLadderRequestHandler().HandleRequest(request));
		}
	}
}
