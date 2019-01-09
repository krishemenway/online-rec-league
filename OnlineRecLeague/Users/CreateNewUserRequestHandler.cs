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
			IUserProfileFactory userProfileFactory = null,
			IConfirmEmailSender confirmEmailSender = null)
		{
			_userStore = userStore ?? new UserStore();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
			_userProfileFactory = userProfileFactory ?? new UserProfileFactory();
			_confirmEmailSender = confirmEmailSender ?? new ConfirmEmailSender();
		}

		public Result<IUserProfile> HandleRequest(CreateNewUserRequest request, ISession session)
		{
			request.JoinTime = TimeZoneInfo.FindSystemTimeZoneById(request.DefaultTimezone).CurrentTime();

			var newUser = _userStore.CreateNewUser(request);
			_userSessionStore.SetUserInSession(session, newUser);
			_confirmEmailSender.SendConfirmEmail(newUser);

			var profile = _userProfileFactory.CreateProfile(newUser, session);
			return Result<IUserProfile>.Successful(profile);
		}

		private readonly IUserStore _userStore;
		private readonly IUserSessionStore _userSessionStore;
		private readonly IUserProfileFactory _userProfileFactory;
		private readonly IConfirmEmailSender _confirmEmailSender;
	}
}
