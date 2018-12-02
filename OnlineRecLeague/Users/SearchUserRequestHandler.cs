using OnlineRecLeague.CommonDataTypes;

namespace OnlineRecLeague.Users
{
	public interface ISearchUserRequestHandler
	{
		Result<SearchUserResponse> HandleRequest(SearchUserRequest request);
	}

	public class SearchUserRequestHandler : ISearchUserRequestHandler
	{
		public Result<SearchUserResponse> HandleRequest(SearchUserRequest request)
		{
			return Result<SearchUserResponse>.Successful(new SearchUserResponse());
		}
	}
}
