using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Ladders;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.LadderChallenges
{
	[ApiController]
	[Route("api/ladders")]
	[RequiresUserInSession]
	public class SetLadderMatchTimeController : ControllerBase
	{
		public SetLadderMatchTimeController(
			ILadderStore ladderStore = null,
			ILadderChallengeStore ladderChallengeStore = null)
		{
			_ladderStore = ladderStore ?? new LadderStore();
			_ladderChallengeStore = ladderChallengeStore ?? new LadderChallengeStore();
		}

		[HttpPost(nameof(SetLadderMatchTime))]
		[ProducesResponseType(200, Type = typeof(Result))]
		public ActionResult<Result> SetLadderMatchTime([FromBody] SetLadderMatchTimeRequest request)
		{
			// TODO: validate user can set match time
			_ladderChallengeStore.SetMatchTime(request.LadderChallengeId, request.MatchTime);
			return Result.Successful();
		}

		private readonly ILadderStore _ladderStore;
		private readonly ILadderChallengeStore _ladderChallengeStore;
	}
}
