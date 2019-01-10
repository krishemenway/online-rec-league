using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using System;

namespace OnlineRecLeague
{
	[Route("api/service")]
	public class ServiceController : Controller
	{
		[HttpGet("exception")]
		[ProducesResponseType(500, Type = typeof(Result))]
		public IActionResult Exception()
		{
			throw new StatusException();
		}

		[HttpGet("")]
		[ProducesResponseType(200, Type = typeof(Result))]
		public IActionResult Status()
		{
			return Json(Result.Successful());
		}

		private class StatusException : Exception
		{
			public StatusException() : base("Service Test Exception") { }
		}
	}
}
