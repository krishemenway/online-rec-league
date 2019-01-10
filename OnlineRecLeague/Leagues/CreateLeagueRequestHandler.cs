using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Leagues
{
	internal class CreateLeagueRequestHandler
	{
		public CreateLeagueRequestHandler(
			ILeagueStore leagueStore = null,
			ILeagueViewModelFactory leagueViewModelFactory = null)
		{
			_leagueStore = leagueStore ?? new LeagueStore();
			_leagueViewModelFactory = leagueViewModelFactory ?? new LeagueViewModelFactory();
		}

		public Result<LeagueViewModel> HandleRequest(CreateLeagueRequest request, IUser userFromSession)
		{
			var league = _leagueStore.Create(request);
			var viewModel = _leagueViewModelFactory.Create(league, userFromSession);

			return Result<LeagueViewModel>.Successful(viewModel);
		}

		private readonly ILeagueStore _leagueStore;
		private readonly ILeagueViewModelFactory _leagueViewModelFactory;
	}
}
