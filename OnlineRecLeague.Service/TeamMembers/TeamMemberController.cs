using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.TeamMembers
{
	[Route("api/teams")]
	public class TeamMemberController : Controller
	{
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
