using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace OnlineRecLeague.Users
{
	[Route("api/users")]
	public class UserController : Controller
	{
		[HttpPost("join")]
		public IActionResult Join([FromBody]CreateNewUserRequest request)
		{
			return Json(new UserRepository().CreateUser(request, HttpContext.Session));
		}

		[HttpGet("profile")]
		public IActionResult Profile([FromQuery]Guid userId)
		{
			return Json(new UserRepository().FindProfile(userId, UserFromSession));
		}

		[HttpGet("search")]
		public IActionResult Search([FromQuery]string nickName)
		{
			throw new NotImplementedException();
		}

		[HttpPost("login")]
		public IActionResult Login([FromQuery]Guid userId)
		{
			throw new NotImplementedException();
		}

		public IUser UserFromSession => new UserSessionStore().FindUserOrThrow(HttpContext.Session);
	}
}
