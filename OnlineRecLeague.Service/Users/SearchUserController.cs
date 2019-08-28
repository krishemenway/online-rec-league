using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users.Profiles;
using System.Linq;

namespace OnlineRecLeague.Users
{
	[ApiController]
	[Route("api/users")]
	public class SearchUserController : ControllerBase
	{
		public SearchUserController(
			IUserStore userStore = null,
			IUserProfileFactory userProfileFactory = null)
		{
			_userStore = userStore ?? new UserStore();
			_userProfileFactory = userProfileFactory ?? new UserProfileFactory();
		}

		[HttpPost(nameof(Search))]
		[ProducesResponseType(200, Type = typeof(Result<SearchUserResponse>))]
		public ActionResult<Result<SearchUserResponse>> Search([FromBody] SearchUserRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Query))
			{
				return Result<SearchUserResponse>.Failure("Missing search query");
			}

			var searchUsersResponse = new SearchUserResponse
				{
					Users = _userStore
						.FindUsersByQuery(request.Query)
						.Select(user => _userProfileFactory.CreateProfile(user, HttpContext.Session))
						.ToList(),
				};

			return Result<SearchUserResponse>.Successful(searchUsersResponse);
		}

		private readonly IUserStore _userStore;
		private readonly IUserProfileFactory _userProfileFactory;
	}
}
