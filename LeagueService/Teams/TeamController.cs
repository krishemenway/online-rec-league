using LeagueService.Users;
using Microsoft.AspNetCore.Mvc;

namespace LeagueService.Teams
{
	[Route("api/teams")]
	public class TeamController : Controller
	{
		[RequiresLoggedInUser]
		[HttpPost("create")]
		public IActionResult CreateTeam(CreateTeamRequest createTeamRequest)
		{
			return Json(new TeamRepository().CreateTeam(createTeamRequest, UserFromSession));
		}

		public IUser UserFromSession => new UserSessionStore().FindUser(HttpContext.Session);
	}

	public class CreateTeamRequest
	{
		public string Name { get; set; }
	}
}
