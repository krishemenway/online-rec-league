using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Leagues
{
	internal class CreateLeagueRequestHandler
	{
		public CreateLeagueRequestHandler(ILeagueStore leagueStore = null)
		{
			_leagueStore = leagueStore ?? new LeagueStore();
		}

		public Result HandleRequest(CreateLeagueRequest request, IUser userFromSession)
		{
			_leagueStore.Create(request);
			return Result.Successful();
		}

		private readonly ILeagueStore _leagueStore;
	}
}
