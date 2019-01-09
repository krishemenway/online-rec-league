using Microsoft.AspNetCore.Http;
using OnlineRecLeague.CommonDataTypes;
using OnlineRecLeague.Users.Profiles;

namespace OnlineRecLeague.Users
{
	public interface IFindProfileRequestHandler
	{
		Result<IUserProfile> HandleRequest(FindProfileRequest request, ISession session);
	}

	internal class FindProfileRequestHandler : IFindProfileRequestHandler
	{
		internal FindProfileRequestHandler(
			IUserStore userStore = null,
			IUserProfileFactory userProfileFactory = null)
		{
			_userStore = userStore ?? new UserStore();
			_userProfileFactory = userProfileFactory ?? new UserProfileFactory();
		}

		public Result<IUserProfile> HandleRequest(FindProfileRequest request, ISession session)
		{
			if (!_userStore.TryFindUserById(request.UserId, out var user))
			{
				return Result<IUserProfile>.Failure("User does not exist");
			}

			var profile = _userProfileFactory.CreateProfile(user, session);
			return Result<IUserProfile>.Successful(profile);
		}

		private readonly IUserStore _userStore;
		private readonly IUserProfileFactory _userProfileFactory;
	}
}
