using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;

namespace OnlineRecLeague
{
	[Route("api/service")]
	public class ServiceStatusController : ControllerBase
	{
		[HttpGet("")]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> Status()
		{
			return Result.Successful();
		}
	}
}
