using OnlineRecLeague.Users;
using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.CommonDataTypes;

namespace OnlineRecLeague.Teams
{
	[Route("api/teams")]
	public class TeamController : Controller
	{
		[HttpPost("create")]
		[RequiresUserInSession]
		[ProducesResponseType(200, Type = typeof(Result<ITeamProfile>))]
		public IActionResult CreateTeam(CreateTeamRequest request)
		{
			return Json(new CreateTeamRequestHandler().CreateTeam(request, UserFromSession));
		}

		[HttpPost("invite")]
		[RequiresUserInSession]
		[ProducesResponseType(200, Type = typeof(Result))]
		public IActionResult Invite(InviteToTeamRequest request)
		{
			return Json(new InviteToTeamRequestHandler().HandleRequest(request, UserFromSession));
		}

		[HttpPost("join")]
		[RequiresUserInSession]
		[ProducesResponseType(200, Type = typeof(Result))]
		public IActionResult Join(JoinTeamRequest request)
		{
			return Json(new JoinTeamRequestHandler().HandleRequest(request, UserFromSession));
		}

		private IUser UserFromSession => new UserSessionStore().FindUserOrThrow(HttpContext.Session);
	}
}
