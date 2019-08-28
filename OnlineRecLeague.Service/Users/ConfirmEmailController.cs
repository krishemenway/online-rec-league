using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;

namespace OnlineRecLeague.Users
{
	[ApiController]
	[Route("api/users")]
	[RequiresUserInSession]
	public class ConfirmEmailController : ControllerBase
	{
		public ConfirmEmailController(
			IUserStore userStore = null,
			IUserSessionStore userSessionStore = null)
		{
			_userStore = userStore ?? new UserStore();
			_userFromSession = userSessionStore ?? new UserSessionStore();
		}

		[HttpPost(nameof(ConfirmEmail))]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> ConfirmEmail([FromBody] ConfirmEmailRequest request)
		{
			var userFromSession = _userFromSession.FindUserOrThrow(HttpContext.Session);

			if (!request.EmailConfirmationCode.HasValue || request.EmailConfirmationCode.Value != userFromSession.EmailConfirmationCode)
			{
				return Result.Failure("Invalid email confirmation code");
			}

			_userStore.EmailConfirmed(userFromSession, userFromSession.DefaultTimezone.CurrentTime());

			return Result.Successful();
		}

		private readonly IUserSessionStore _userFromSession;
		private readonly IUserStore _userStore;
	}
}
