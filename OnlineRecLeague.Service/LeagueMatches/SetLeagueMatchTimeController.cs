using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Leagues;
using OnlineRecLeague.Users;
using System;

namespace OnlineRecLeague.LeagueMatches
{
	[ApiController]
	[Route("api/leagues")]
	[RequiresUserInSession]
	public class SetLeagueMatchTimeController : ControllerBase
	{
		public SetLeagueMatchTimeController(
			ILeagueStore leagueStore = null,
			ILeagueMatchStore leagueMatchStore = null)
		{
			_leagueStore = leagueStore ?? new LeagueStore();
			_leagueMatchStore = leagueMatchStore ?? new LeagueMatchStore();
		}

		[HttpPost(nameof(SetMatchTime))]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> SetMatchTime([FromBody] SetLeagueMatchTimeRequest request)
		{
			// TODO: validate user can set match time
			_leagueMatchStore.SetMatchTime(request.LeagueMatchId, request.MatchTime);
			return Result.Successful();
		}

		private readonly ILeagueStore _leagueStore;
		private readonly ILeagueMatchStore _leagueMatchStore;
	}

	public class SetLeagueMatchTimeRequest
	{
		public Guid LeagueMatchId { get; set; }
		public DateTimeOffset MatchTime { get; set; }
	}
}
