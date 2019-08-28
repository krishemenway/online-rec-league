using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users.Profiles;

namespace OnlineRecLeague.Users
{
	[ApiController]
	[Route("api/users")]
	public class FindProfileController : ControllerBase
	{
		public FindProfileController(
			IUserStore userStore = null,
			IUserProfileFactory userProfileFactory = null)
		{
			_userStore = userStore ?? new UserStore();
			_userProfileFactory = userProfileFactory ?? new UserProfileFactory();
		}

		[HttpGet(nameof(Profile))]
		[ProducesResponseType(200, Type = typeof(Result<IUserProfile>))]
		public ActionResult<Result<IUserProfile>> Profile([FromQuery] FindProfileRequest request)
		{
			if (!_userStore.TryFindUserById(request.UserId, out var user))
			{
				return Result<IUserProfile>.Failure("User does not exist");
			}

			var profile = _userProfileFactory.CreateProfile(user, HttpContext.Session);
			return Result<IUserProfile>.Successful(profile);
		}

		private readonly IUserStore _userStore;
		private readonly IUserProfileFactory _userProfileFactory;
	}
}
