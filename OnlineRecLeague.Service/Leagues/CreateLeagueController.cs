using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Leagues
{
	[ApiController]
	[Route("api/leagues")]
	[RequiresUserInSession]
	public class CreateLeagueController : ControllerBase
	{
		public CreateLeagueController(
			ILeagueStore leagueStore = null,
			ILeagueViewModelFactory leagueViewModelFactory = null,
			IUserSessionStore userSessionStore = null)
		{
			_leagueStore = leagueStore ?? new LeagueStore();
			_leagueViewModelFactory = leagueViewModelFactory ?? new LeagueViewModelFactory();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
		}

		[HttpPost(nameof(Create))]
		[ProducesResponseType(200, Type = typeof(Result<LeagueViewModel>))]
		public ActionResult<Result<LeagueViewModel>> Create([FromBody] CreateLeagueRequest request)
		{
			var userFromSession = _userSessionStore.FindUserOrThrow(ControllerContext.HttpContext.Session);
			var league = _leagueStore.Create(request);
			var viewModel = _leagueViewModelFactory.Create(league, userFromSession);

			return Result<LeagueViewModel>.Successful(viewModel);
		}

		private readonly ILeagueStore _leagueStore;
		private readonly ILeagueViewModelFactory _leagueViewModelFactory;
		private readonly IUserSessionStore _userSessionStore;
	}
}
