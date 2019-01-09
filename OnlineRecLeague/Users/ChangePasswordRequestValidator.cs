using OnlineRecLeague.CommonDataTypes;

namespace OnlineRecLeague.Users
{
	internal interface IChangePasswordRequestValidator
	{
		Result Validate(ChangePasswordRequest request, IUser user);
	}

	internal class ChangePasswordRequestValidator : IChangePasswordRequestValidator
	{
		public ChangePasswordRequestValidator(IUserPasswordValidator userPasswordValidator = null)
		{
			_userPasswordValidator = userPasswordValidator ?? new UserPasswordValidator();
		}

		public Result Validate(ChangePasswordRequest request, IUser user)
		{
			if (!_userPasswordValidator.Validate(user, request.CurrentPassword))
			{
				return Result.Failure("Current password is incorrect");
			}

			if (request.NewPassword != request.ConfirmNewPassword)
			{
				return Result.Failure("Confirmation password did not match, please retype your new password");
			}

			return _userPasswordValidator.MeetsPasswordRequirements(request.NewPassword);
		}

		private readonly IUserPasswordValidator _userPasswordValidator;
	}
}
