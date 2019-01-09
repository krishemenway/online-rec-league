using OnlineRecLeague.CommonDataTypes;

namespace OnlineRecLeague.Users
{
	internal class ChangePasswordRequestHandler
	{
		public ChangePasswordRequestHandler(
			IChangePasswordRequestValidator changePasswordRequestValidator = null,
			IUserPasswordValidator userPasswordValidator = null,
			IUserStore userStore = null)
		{
			_changePasswordRequestValidator = changePasswordRequestValidator ?? new ChangePasswordRequestValidator();
			_userPasswordValidator = userPasswordValidator ?? new UserPasswordValidator();
			_userStore = userStore ?? new UserStore();
		}

		public Result HandleRequest(ChangePasswordRequest request, IUser userFromSession)
		{
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
	}
}
