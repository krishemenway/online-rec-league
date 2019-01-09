using Microsoft.AspNetCore.Http;
using OnlineRecLeague.CommonDataTypes;

namespace OnlineRecLeague.Users
{
	internal interface ILoginRequestHandler
	{
		Result HandleRequest(LoginRequest request, ISession session);
	}

	internal class LoginRequestHandler : ILoginRequestHandler
	{
		public LoginRequestHandler(
			IUserSessionStore userSessionStore = null,
			IUserStore userStore = null,
			ISettings settings = null)
		{
			_userSessionStore = userSessionStore ?? new UserSessionStore();
			_userStore = userStore ?? new UserStore();
			_settings = settings ?? Program.Settings;
		}

		public Result HandleRequest(LoginRequest request, ISession session)
		{
			if (!_userStore.TryFindUserByEmail(request.EmailAddress, out var user))
			{
				return Result.Failure(InvalidLoginRequestMessage);
			}

			if (BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
			{
				return Result.Failure(InvalidLoginRequestMessage);
			}

			_userSessionStore.SetUserInSession(session, user);
			return Result.Successful();
		}

		internal const string InvalidLoginRequestMessage = "Email address or password was incorrect";

		private readonly IUserSessionStore _userSessionStore;
		private readonly IUserStore _userStore;
		private readonly ISettings _settings;
	}
}
