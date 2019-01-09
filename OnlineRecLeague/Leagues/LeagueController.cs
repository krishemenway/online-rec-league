using OnlineRecLeague.Users;
using Microsoft.AspNetCore.Mvc;

namespace OnlineRecLeague.Leagues
{
	[Route("api/leagues")]
	public class LeagueController : Controller
	{
		[HttpPost("create")]
		[RequiresUserInSession]
		public IActionResult Create(CreateLeagueRequest request)
		{
			return Json(new CreateLeagueRequestHandler().HandleRequest(request, UserFromSession));
		}

		private IUser UserFromSession => new UserSessionStore().FindUserOrThrow(HttpContext.Session);
	}
}
