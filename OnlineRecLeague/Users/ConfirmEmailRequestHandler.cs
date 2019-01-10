using OnlineRecLeague.DataTypes;
using OnlineRecLeague.CoreExtensions;

namespace OnlineRecLeague.Users
{
	public class ConfirmEmailRequestHandler
	{
		public ConfirmEmailRequestHandler(IUserStore userStore = null)
		{
			_userStore = userStore ?? new UserStore();
		}

		public Result HandleRequest(ConfirmEmailRequest request, IUser userFromSession)
		{
			if (!request.EmailConfirmationCode.HasValue)
			{
				return Result.Failure("Missing email confirmation code");
			}

			if (request.EmailConfirmationCode.Value != userFromSession.EmailConfirmationCode)
			{
				return Result.Failure("Invalid email confirmation code");
			}

			_userStore.EmailConfirmed(userFromSession, userFromSession.DefaultTimezone.CurrentTime());

			return Result.Successful();
		}

		private readonly IUserStore _userStore;
	}
}
