using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.CommonDataTypes;
using OnlineRecLeague.Users.Profiles;

namespace OnlineRecLeague.Users
{
	[Route("api/users")]
	public class UserController : Controller
	{
		[HttpPost("join")]
		[ProducesResponseType(200, Type = typeof(Result<IUserProfile>))]
		public IActionResult Join([FromBody]CreateUserRequest request)
		{
			return Json(new CreateUserRequestHandler().HandleRequest(request, HttpContext.Session));
		}

		[HttpGet("profile")]
		[ProducesResponseType(200, Type = typeof(Result<IUserProfile>))]
		public IActionResult Profile([FromQuery]FindProfileRequest request)
		{
			return Json(new FindProfileRequestHandler().HandleRequest(request, HttpContext.Session));
		}

		[HttpGet("search")]
		[ProducesResponseType(200, Type = typeof(Result<SearchUserResponse>))]
		public IActionResult Search([FromQuery]SearchUserRequest request)
		{
			return Json(new SearchUserRequestHandler().HandleRequest(request, HttpContext.Session));
		}

		[HttpPost("login")]
		[ProducesResponseType(200, Type = typeof(Result))]
		public IActionResult Login([FromQuery]LoginRequest request)
		{
			return Json(new LoginRequestHandler().HandleRequest(request, HttpContext.Session));
		}

		[HttpPost("confirm")]
		[RequiresUserInSession]
		[ProducesResponseType(200, Type = typeof(Result))]
		public IActionResult Confirm([FromBody]ConfirmEmailRequest request)
		{
			return Json(new ConfirmEmailRequestHandler().HandleRequest(request, UserFromSession));
		}

		[HttpPost("changepassword")]
		[RequiresUserInSession]
		[ProducesResponseType(200, Type = typeof(Result))]
		public IActionResult ChangePassword([FromBody]ChangePasswordRequest request)
		{
			return Json(new ChangePasswordRequestHandler().HandleRequest(request, UserFromSession));
		}

		[HttpPost("sendconfirmemail")]
		[RequiresUserInSession]
		[ProducesResponseType(200, Type = typeof(Result))]
		public IActionResult SendConfirmEmail([FromBody]SendEmailConfirmationRequest request)
		{
			return Json(new SendEmailConfirmationRequestHandler().HandleRequest(UserFromSession));
		}

		private IUser UserFromSession => new UserSessionStore().FindUserOrThrow(this.HttpContext.Session);
	}
}
