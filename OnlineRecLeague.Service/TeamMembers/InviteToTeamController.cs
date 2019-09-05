using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Email;
using OnlineRecLeague.Service.DataTypes;
using OnlineRecLeague.Teams;
using OnlineRecLeague.Users;
using System.Collections.Generic;

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
				_emailSender.SendEmail(email, "Invite to Team", $"Invite to Team ({team.Name}) Body");
			}
			
			return Result.Successful();
		}

		private readonly ITeamStore _teamStore;
		private readonly IEmailSender _emailSender;
	}

	public class InviteToTeamRequest
	{
		public Id<Team> TeamId { get; set; }
		public IReadOnlyList<string> Emails { get; set; }
	}
}
