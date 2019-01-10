using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Email;
using OnlineRecLeague.Teams;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.TeamMembers
{
	internal class InviteToTeamRequestHandler
	{
		public InviteToTeamRequestHandler(
			ITeamStore teamStore = null,
			IEmailSender emailSender = null)
		{
			_teamStore = teamStore ?? new TeamStore();
			_emailSender = emailSender ?? new EmailSender();
		}

		public Result HandleRequest(InviteToTeamRequest request, IUser loggedInUser)
		{
			if (!_teamStore.TryFindTeam(request.TeamId, out var team))
			{
				return Result.Failure("Unable to find team to invite into.");
			}

			_emailSender.SendEmail("Invite to Team", "Invite to Team Body");
			return Result.Successful();
		}

		private readonly ITeamStore _teamStore;
		private readonly IEmailSender _emailSender;
	}
}
