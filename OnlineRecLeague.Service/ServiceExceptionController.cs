using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using System;

namespace OnlineRecLeague
{
	[Route("api/service")]
	public class ServiceExceptionController : ControllerBase
	{
		[HttpGet("exception")]
		[ProducesResponseType(500, Type = typeof(Result))]
		public ActionResult Exception()
		{
			throw new StatusException();
		}

		private class StatusException : Exception
		{
			public StatusException() : base("Service Test Exception") { }
		}
	}
}
