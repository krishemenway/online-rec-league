using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;
using System;

namespace OnlineRecLeague.LeagueMatches
{
	[ApiController]
	[Route("api/leaguematches")]
	public class ReportResultsController : ControllerBase
	{
		[RequiresUserInSession]
		[HttpPost(nameof(ReportResults))]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> ReportResults([FromBody] ReportResultsRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
