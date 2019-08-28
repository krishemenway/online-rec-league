using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Email;
using OnlineRecLeague.Teams;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.TeamMembers
{
	[ApiController]
	[Route("api/teams")]
	[RequiresUserInSession]
	public class InviteToTeamController : ControllerBase
	{
		public InviteToTeamController(
			ITeamStore teamStore = null,
			IEmailSender emailSender = null)
		{
			_teamStore = teamStore ?? new TeamStore();
			_emailSender = emailSender ?? new EmailSender();
		}

		[HttpPost(nameof(Invite))]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> Invite([FromBody] InviteToTeamRequest request)
		{
			if (!_teamStore.TryFindTeam(request.TeamId, out var team))
			{
				return Result.Failure("Unable to find team to invite into.");
			}

			foreach (var email in request.Emails)
			{
				_emailSender.SendEmail(email, "Invite to Team", "Invite to Team Body");
			}
			
			return Result.Successful();
		}

		private readonly ITeamStore _teamStore;
		private readonly IEmailSender _emailSender;
	}
}
