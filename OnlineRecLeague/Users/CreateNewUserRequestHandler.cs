using Microsoft.AspNetCore.Http;
using OnlineRecLeague.CommonDataTypes;
using OnlineRecLeague.CoreExtensions;
using OnlineRecLeague.Users.Profiles;
using System;

namespace OnlineRecLeague.Users
{
	public interface ICreateNewUserRequestHandler
	{
		Result<IUserProfile> HandleRequest(CreateNewUserRequest request, ISession session);
	}

	internal class CreateNewUserRequestHandler : ICreateNewUserRequestHandler
	{
		internal CreateNewUserRequestHandler(
			IUserStore userStore = null,
			IUserSessionStore userSessionStore = null,
			IUserProfileFactory userProfileFactory = null)
		{
			_userStore = userStore ?? new UserStore();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
			_userProfileFactory = userProfileFactory ?? new UserProfileFactory();
		}

		public Result<IUserProfile> HandleRequest(CreateNewUserRequest request, ISession session)
		{
			request.JoinTime = TimeZoneInfo.FindSystemTimeZoneById(request.DefaultTimezone).CurrentTime();

			var newUser = _userStore.CreateNewUser(request);
			_userSessionStore.SetLoggedInUser(session, newUser);

			var userProfile = _userProfileFactory.CreateProfile(newUser, UserProfileType.PersonalProfile);
			return Result<IUserProfile>.Successful(userProfile);
		}

		private readonly IUserStore _userStore;
		private readonly IUserSessionStore _userSessionStore;
		private readonly IUserProfileFactory _userProfileFactory;
	}
}
