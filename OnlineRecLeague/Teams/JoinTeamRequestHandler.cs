using System;
using System.Linq;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Teams
{
	internal class JoinTeamRequestHandler
	{
		public JoinTeamRequestHandler(
			ITeamStore teamStore = null,
			IInviteToTeamStore inviteToTeamStore = null,
			ITeamMemberStore teamMemberStore = null)
		{
			_teamStore = teamStore ?? new TeamStore();
			_inviteToTeamStore = inviteToTeamStore ?? new InviteToTeamStore();
			_teamMemberStore = teamMemberStore ?? new TeamMemberStore();
		}

		public Result HandleRequest(JoinTeamRequest joinTeamRequest, IUser userFromSession)
		{
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
	}
}
