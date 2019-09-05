using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Teams
{
	[ApiController]
	[Route("api/teams")]
	[RequiresUserInSession]
	public class CreateTeamController : ControllerBase
	{
		public CreateTeamController(
			ITeamStore teamStore = null,
			ITeamProfileFactory teamProfileFactory = null,
			IUserSessionStore userSessionStore = null)
		{
			_teamStore = teamStore ?? new TeamStore();
			_teamProfileFactory = teamProfileFactory ?? new TeamProfileFactory();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
		}

		[HttpPost(nameof(CreateTeam))]
		[ProducesResponseType(200, Type = typeof(Result<ITeamProfile>))]
		public ActionResult<Result<ITeamProfile>> CreateTeam([FromBody] CreateTeamRequest request)
		{
			var userFromSession = _userSessionStore.FindUserOrThrow(HttpContext.Session);
			var team = _teamStore.CreateTeam(request, userFromSession);
			var teamProfile = _teamProfileFactory.Create(team);

			return Result<ITeamProfile>.Successful(teamProfile);
		}

		private readonly ITeamStore _teamStore;
		private readonly ITeamProfileFactory _teamProfileFactory;
		private readonly IUserSessionStore _userSessionStore;
	}

	public class CreateTeamRequest
	{
		public string Name { get; set; }
		public string ProfileContent { get; set; }
	}
}
