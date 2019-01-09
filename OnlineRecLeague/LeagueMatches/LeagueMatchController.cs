using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.LeagueMatches
{
	[Route("api/leaguematches")]
	public class LeagueMatchController : Controller
	{
		[RequiresUserInSession]
		[HttpPost("ReportResults")]
		public IActionResult ReportLeagueMatchResults(ReportLeagueMatchResultsRequest request)
		{
			return Json(new ReportLeagueMatchResultsRequestHandler().HandleRequest(request, UserFromSession));
		}

		private IUser UserFromSession => new UserSessionStore().FindUserOrThrow(HttpContext.Session);
	}
}
