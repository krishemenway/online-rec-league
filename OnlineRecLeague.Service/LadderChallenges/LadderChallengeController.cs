using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;
using System;

namespace OnlineRecLeague.LadderChallenges
{
	[ApiController]
	[Route("api/ladders")]
	[RequiresUserInSession]
	public class LadderChallengeController : ControllerBase
	{
		[HttpPost(nameof(CreateChallenge))]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> CreateChallenge([FromBody] LadderChallengeRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
