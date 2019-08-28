using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users.Profiles;
using System;

namespace OnlineRecLeague.Users
{
	[ApiController]
	[Route("api/users")]
	public class CreateUserController : ControllerBase
	{
		public CreateUserController(
			IUserStore userStore = null,
			IUserSessionStore userSessionStore = null,
			IUserPasswordValidator userPasswordValidator = null,
			ICreateUserRequestValidator createUserRequestValidator = null,
			ISendEmailConfirmation sendEmailConfirmation = null)
		{
			_userStore = userStore ?? new UserStore();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
			_userPasswordValidator = userPasswordValidator ?? new UserPasswordValidator();
			_createUserRequestValidator = createUserRequestValidator ?? new CreateUserRequestValidator();
			_sendEmailConfirmation = sendEmailConfirmation ?? new SendEmailConfirmationController();
		}

		[HttpPost(nameof(Join))]
		[ProducesResponseType(200, Type = typeof(Result<IUserProfile>))]
		public ActionResult<Result> Join([FromBody] CreateUserRequest request)
		{
			var validationResult = _createUserRequestValidator.Validate(request);
			if (!validationResult.Success)
			{
				return validationResult;
			}

			request.JoinTime = TimeZoneInfo.FindSystemTimeZoneById(request.DefaultTimezone).CurrentTime();
			request.Password = _userPasswordValidator.CreatePassword(request.Email, request.Password);

			var newUser = _userStore.CreateNewUser(request);
			_userSessionStore.SetUserInSession(HttpContext.Session, newUser);
			_sendEmailConfirmation.SendEmailConfirmation();

			return Result.Successful();
		}

		private readonly IUserStore _userStore;
		private readonly IUserSessionStore _userSessionStore;
		private readonly IUserPasswordValidator _userPasswordValidator;
		private readonly ICreateUserRequestValidator _createUserRequestValidator;
		private readonly ISendEmailConfirmation _sendEmailConfirmation;
	}
}
