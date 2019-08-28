using Microsoft.AspNetCore.Identity;
using OnlineRecLeague.DataTypes;

namespace OnlineRecLeague.Users
{
	public interface IUserPasswordValidator
	{
		string CreatePassword(string email, string password);
		bool Validate(IUser user, string password);
		Result MeetsPasswordRequirements(string password);
	}

	internal class UserPasswordValidator : IUserPasswordValidator
	{
		public UserPasswordValidator(
			IUserStore userStore = null,
			IPasswordHasher<string> passwordHasher = null)
		{
			_userStore = userStore ?? new UserStore();
			_passwordHasher = passwordHasher ?? new PasswordHasher<string>();
		}

		public Result MeetsPasswordRequirements(string password)
		{
			if (string.IsNullOrWhiteSpace(password))
			{
				return Result.Failure("Must provide a password");
			}

			if (!ValidPasswordLength.ContainsInclusive(password.Length))
			{
				return Result.Failure("Password must be at least 8 characters long");
			}

			return Result.Successful();
		}

		public string CreatePassword(string email, string password)
		{
			return _passwordHasher.HashPassword(email.ToLower(), password);
		}

		public bool Validate(IUser user, string password)
		{
			var result = _passwordHasher.VerifyHashedPassword(user.Email, user.PasswordHash, password);

			if (result == PasswordVerificationResult.Failed)
			{
				return false;
			}

			if (result == PasswordVerificationResult.SuccessRehashNeeded)
			{
				_userStore.UpdatePassword(user, CreatePassword(user.Email, password));
			}

			return true;
		}

		internal static readonly Range<int> ValidPasswordLength = new Range<int>(8, 512);

		private readonly IUserStore _userStore;
		private readonly IPasswordHasher<string> _passwordHasher;
	}
}
