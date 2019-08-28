using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;

namespace OnlineRecLeague.Users
{
	[ApiController]
	[Route("api/users")]
	public class LoginController : ControllerBase
	{
		public LoginController(
			IUserSessionStore userSessionStore = null,
			IUserStore userStore = null,
			IUserPasswordValidator userPasswordValidator = null)
		{
			_userSessionStore = userSessionStore ?? new UserSessionStore();
			_userStore = userStore ?? new UserStore();
			_userPasswordValidator = userPasswordValidator ?? new UserPasswordValidator();
		}

		[HttpPost("login")]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> Login([FromBody] LoginRequest request)
		{
			if (!_userStore.TryFindUserByEmail(request.EmailAddress, out var user) || !_userPasswordValidator.Validate(user, request.Password))
			{
				return Result.Failure(InvalidLoginRequestMessage);
			}

			_userSessionStore.SetUserInSession(HttpContext.Session, user);
			return Result.Successful();
		}

		internal const string InvalidLoginRequestMessage = "Email address or password was incorrect";

		private readonly IUserSessionStore _userSessionStore;
		private readonly IUserStore _userStore;
		private readonly IUserPasswordValidator _userPasswordValidator;
	}
}
