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
		public IActionResult Join([FromBody]CreateNewUserRequest request)
		{
			return Json(new CreateNewUserRequestHandler().HandleRequest(request, HttpContext.Session));
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
			return Json(new SearchUserRequestHandler().HandleRequest(request));
		}

		[HttpPost("login")]
		[ProducesResponseType(200, Type = typeof(Result))]
		public IActionResult Login([FromQuery]LoginRequest request)
		{
			return Json(new LoginRequestHandler().HandleRequest(request, HttpContext.Session));
		}
	}
}
