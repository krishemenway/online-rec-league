using Microsoft.AspNetCore.Http;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users.Profiles;
using System.Linq;

namespace OnlineRecLeague.Users
{
	public interface ISearchUserRequestHandler
	{
		Result<SearchUserResponse> HandleRequest(SearchUserRequest request, ISession session);
	}

	public class SearchUserRequestHandler : ISearchUserRequestHandler
	{
		public SearchUserRequestHandler(
			IUserStore userStore = null,
			IUserProfileFactory userProfileFactory = null)
		{
			_userStore = userStore ?? new UserStore();
			_userProfileFactory = userProfileFactory ?? new UserProfileFactory();
		}

		public Result<SearchUserResponse> HandleRequest(SearchUserRequest request, ISession session)
		{
			if (string.IsNullOrWhiteSpace(request.Query))
			{
				return Result<SearchUserResponse>.Failure("Missing search query");
			}

			var searchUsersResponse = new SearchUserResponse
				{
					Users = _userStore
						.FindUsersByQuery(request.Query)
						.Select(user => _userProfileFactory.CreateProfile(user, session))
						.ToList(),
				};

			return Result<SearchUserResponse>.Successful(searchUsersResponse);
		}

		private readonly IUserStore _userStore;
		private readonly IUserProfileFactory _userProfileFactory;
	}
}
