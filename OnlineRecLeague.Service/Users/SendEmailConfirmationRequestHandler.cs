using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Email;

namespace OnlineRecLeague.Users
{
	internal interface ISendEmailConfirmationRequestHandler
	{
		Result HandleRequest(IUser userFromSession);
	}

	internal class SendEmailConfirmationRequestHandler : ISendEmailConfirmationRequestHandler
	{
		public SendEmailConfirmationRequestHandler(
			IUserStore userStore = null,
			IEmailSender emailSender = null)
		{
			_userStore = userStore ?? new UserStore();
			_emailSender = emailSender ?? new EmailSender();
		}

		public Result HandleRequest(IUser userFromSession)
		{
			_emailSender.SendEmail(userFromSession.Email, "Email Confirmation for OnlineRecLeague", $"{userFromSession.EmailConfirmationCode}");
			return Result.Successful();
		}

		private readonly IUserStore _userStore;
		private readonly IEmailSender _emailSender;
	}
}
