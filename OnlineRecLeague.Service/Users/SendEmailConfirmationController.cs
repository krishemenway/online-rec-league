using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Email;

namespace OnlineRecLeague.Users
{
	public interface ISendEmailConfirmation
	{
		ActionResult<Result> SendEmailConfirmation();
	}

	[ApiController]
	[Route("api/users")]
	[RequiresUserInSession]
	public class SendEmailConfirmationController : ControllerBase, ISendEmailConfirmation
	{
		public SendEmailConfirmationController(
			IUserStore userStore = null,
			IEmailSender emailSender = null,
			IUserSessionStore userSessionStore = null)
		{
			_userStore = userStore ?? new UserStore();
			_emailSender = emailSender ?? new EmailSender();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
		}

		[HttpPost(nameof(SendEmailConfirmation))]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> SendEmailConfirmation()
		{
			var userFromSession = _userSessionStore.FindUserOrThrow(HttpContext.Session);

			_emailSender.SendEmail(userFromSession.Email, "Email Confirmation for OnlineRecLeague", $"{userFromSession.EmailConfirmationCode}");

			return Result.Successful();
		}

		private readonly IUserStore _userStore;
		private readonly IEmailSender _emailSender;
		private readonly IUserSessionStore _userSessionStore;
	}
}
