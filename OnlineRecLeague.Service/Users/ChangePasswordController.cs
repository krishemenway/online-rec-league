using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;

namespace OnlineRecLeague.Users
{
	[ApiController]
	[Route("api/users")]
	[RequiresUserInSession]
	public class ChangePasswordController : ControllerBase
	{
		public ChangePasswordController(
			IChangePasswordRequestValidator changePasswordRequestValidator = null,
			IUserPasswordValidator userPasswordValidator = null,
			IUserStore userStore = null,
			IUserSessionStore userSessionStore = null)
		{
			_changePasswordRequestValidator = changePasswordRequestValidator ?? new ChangePasswordRequestValidator();
			_userPasswordValidator = userPasswordValidator ?? new UserPasswordValidator();
			_userStore = userStore ?? new UserStore();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
		}

		[HttpPost(nameof(ChangePassword))]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> ChangePassword([FromBody] ChangePasswordRequest request)
		{
			var userFromSession = _userSessionStore.FindUserOrThrow(HttpContext.Session);
			var validationResult = _changePasswordRequestValidator.Validate(request, userFromSession);

			if (!validationResult.Success)
			{
				return validationResult;
			}

			_userStore.UpdatePassword(userFromSession, _userPasswordValidator.CreatePassword(userFromSession.Email, request.NewPassword));
			return Result.Successful();
		}

		private readonly IChangePasswordRequestValidator _changePasswordRequestValidator;
		private readonly IUserPasswordValidator _userPasswordValidator;
		private readonly IUserStore _userStore;
		private readonly IUserSessionStore _userSessionStore;
	}
}
