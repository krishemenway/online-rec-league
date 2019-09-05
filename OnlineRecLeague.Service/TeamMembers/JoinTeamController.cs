using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Service.DataTypes;
using OnlineRecLeague.Teams;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.TeamMembers
{
	[ApiController]
	[Route("api/teams")]
	[RequiresUserInSession]
	public class JoinTeamController : ControllerBase
	{
		public JoinTeamController(
			ITeamStore teamStore = null,
			IInviteToTeamStore inviteToTeamStore = null,
			ITeamMemberStore teamMemberStore = null,
			IUserSessionStore userSessionStore = null)
		{
			_teamStore = teamStore ?? new TeamStore();
			_inviteToTeamStore = inviteToTeamStore ?? new InviteToTeamStore();
			_teamMemberStore = teamMemberStore ?? new TeamMemberStore();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
		}

		[HttpPost(nameof(JoinTeam))]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> JoinTeam([FromBody] JoinTeamRequest joinTeamRequest)
		{
			var userFromSession = _userSessionStore.FindUserOrThrow(HttpContext.Session);
			if (!_teamStore.TryFindTeam(joinTeamRequest.TeamId, out var team))
			{
				return Result.Failure("Unable to find team to join");
			}

			var invitesForUser = _inviteToTeamStore.FindOpenInvitesForUsers(userFromSession);
			if (!invitesForUser.Contains(team.TeamId))
			{
				return Result.Failure("Cannot join team without an invite");
			}

			_teamMemberStore.JoinTeam(userFromSession, team);
			_inviteToTeamStore.RemoveInviteToTeam(team, userFromSession.Email);

			return Result.Successful();
		}

		private readonly ITeamStore _teamStore;
		private readonly IInviteToTeamStore _inviteToTeamStore;
		private readonly ITeamMemberStore _teamMemberStore;
		private readonly IUserSessionStore _userSessionStore;
	}

	public class JoinTeamRequest
	{
		public Id<Team> TeamId { get; set; }
	}
}
