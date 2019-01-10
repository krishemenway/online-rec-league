using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.LeagueMatches
{
	public class ReportLeagueMatchResultsRequestHandler
	{
		public Result HandleRequest(ReportLeagueMatchResultsRequest request, IUser loggedInUser)
		{
			return Result.Successful();
		}
	}
}
