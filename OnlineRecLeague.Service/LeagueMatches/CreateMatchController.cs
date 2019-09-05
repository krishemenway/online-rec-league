using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Leagues;
using OnlineRecLeague.LeagueTeams;
using OnlineRecLeague.Service.DataTypes;
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
		public Id<League> LeagueId { get; set; }

		public Id<LeagueTeam> HomeLeagueTeamId { get; set; }
		public Id<LeagueTeam> AwayLeagueTeamId { get; set; }
	}
}
