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
			IUserStore userStore = null)
		{
			_userSessionStore = userSessionStore ?? new UserSessionStore();
			_userStore = userStore ?? new UserStore();
		}

		public Result HandleRequest(LoginRequest request, ISession session)
		{
			if (!_userStore.TryFindUserByEmail(request.EmailAddress, out var user))
			{
				return Result.Failure(InvalidUsernameOrPasswordFailureMessage);
			}

			_userSessionStore.SetLoggedInUser(session, user);
			return Result.Successful();
		}

		internal const string InvalidUsernameOrPasswordFailureMessage = "";

		private readonly IUserSessionStore _userSessionStore;
		private readonly IUserStore _userStore;
	}
}
