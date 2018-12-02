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
			IUserSessionStore userSessionStore = null,
			IUserProfileFactory userProfileFactory = null)
		{
			_userStore = userStore ?? new UserStore();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
			_userProfileFactory = userProfileFactory ?? new UserProfileFactory();
		}

		public Result<IUserProfile> HandleRequest(FindProfileRequest request, ISession session)
		{
			_userSessionStore.TryFindUser(session, out var loggedInUser);

			if (!_userStore.TryFindUserById(request.UserId, out var foundUser))
			{
				return Result<IUserProfile>.Failure("User does not exist");
			}

			var profileType = DetermineProfileType(foundUser, loggedInUser);
			var userProfile = _userProfileFactory.CreateProfile(foundUser, profileType);

			return Result<IUserProfile>.Successful(userProfile);
		}

		private UserProfileType DetermineProfileType(IUser foundUser, IUser loggedInUser)
		{
			if (loggedInUser == null)
			{
				return UserProfileType.StrangerProfile;
			}

			if (foundUser == loggedInUser)
			{
				return UserProfileType.PersonalProfile;
			}

			return UserProfileType.StrangerProfile;
		}

		private readonly IUserStore _userStore;
		private readonly IUserSessionStore _userSessionStore;
		private readonly IUserProfileFactory _userProfileFactory;
	}
}
