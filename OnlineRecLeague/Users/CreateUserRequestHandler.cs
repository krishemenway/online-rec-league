using Microsoft.AspNetCore.Http;
using OnlineRecLeague.CommonDataTypes;
using OnlineRecLeague.CoreExtensions;
using System;

namespace OnlineRecLeague.Users
{
	internal class CreateUserRequestHandler
	{
		public CreateUserRequestHandler(
			IUserStore userStore = null,
			IUserSessionStore userSessionStore = null,
			IUserPasswordValidator userPasswordValidator = null,
			ICreateUserRequestValidator createUserRequestValidator = null,
			ISendEmailConfirmationRequestHandler sendEmailConfirmationRequestHandler = null)
		{
			_userStore = userStore ?? new UserStore();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
			_userPasswordValidator = userPasswordValidator ?? new UserPasswordValidator();
			_createUserRequestValidator = createUserRequestValidator ?? new CreateUserRequestValidator();
			_sendEmailConfirmationRequestHandler = sendEmailConfirmationRequestHandler ?? new SendEmailConfirmationRequestHandler();
		}

		public Result HandleRequest(CreateUserRequest request, ISession session)
		{
			var validationResult = _createUserRequestValidator.Validate(request);
			if (!validationResult.Success)
			{
				return validationResult;
			}

			request.JoinTime = TimeZoneInfo.FindSystemTimeZoneById(request.DefaultTimezone).CurrentTime();
			request.Password = _userPasswordValidator.CreatePassword(request.Email, request.Password);

			var newUser = _userStore.CreateNewUser(request);
			_userSessionStore.SetUserInSession(session, newUser);
			_sendEmailConfirmationRequestHandler.HandleRequest(newUser);

			return Result.Successful();
		}

		private readonly IUserStore _userStore;
		private readonly IUserSessionStore _userSessionStore;
		private readonly IUserPasswordValidator _userPasswordValidator;
		private readonly ICreateUserRequestValidator _createUserRequestValidator;
		private readonly ISendEmailConfirmationRequestHandler _sendEmailConfirmationRequestHandler;
	}
}
