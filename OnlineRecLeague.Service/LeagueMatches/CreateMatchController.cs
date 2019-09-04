using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;
using System;

namespace OnlineRecLeague.LeagueMatches
{
	[ApiController]
	[Route("api/leagues")]
	[RequiresUserInSession]
	public class CreateMatchController : ControllerBase
	{
		[HttpPost(nameof(CreateMatch))]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> CreateMatch([FromBody] CreateMatchRequest request)
		{
			throw new NotImplementedException();
		}
	}

	public class CreateMatchRequest
	{
		public Guid LeagueId { get; set; }

		public Guid HomeLeagueTeamId { get; set; }
		public Guid AwayLeagueTeamId { get; set; }
	}
}
