using Microsoft.AspNetCore.Http;
using OnlineRecLeague.DataTypes;

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
			IUserPasswordValidator userPasswordValidator = null)
		{
			_userSessionStore = userSessionStore ?? new UserSessionStore();
			_userStore = userStore ?? new UserStore();
			_userPasswordValidator = userPasswordValidator ?? new UserPasswordValidator();
		}

		public Result HandleRequest(LoginRequest request, ISession session)
		{
			if (!_userStore.TryFindUserByEmail(request.EmailAddress, out var user) || !_userPasswordValidator.Validate(user, request.Password))
			{
				return Result.Failure(InvalidLoginRequestMessage);
			}

			_userSessionStore.SetUserInSession(session, user);
			return Result.Successful();
		}

		internal const string InvalidLoginRequestMessage = "Email address or password was incorrect";

		private readonly IUserSessionStore _userSessionStore;
		private readonly IUserStore _userStore;
		private readonly IUserPasswordValidator _userPasswordValidator;
	}
}
