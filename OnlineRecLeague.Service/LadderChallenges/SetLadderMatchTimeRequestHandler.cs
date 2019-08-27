using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Ladders;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.LadderChallenges
{
	public class SetLadderMatchTimeRequestHandler
	{
		public SetLadderMatchTimeRequestHandler(
			ILadderStore ladderStore = null,
			ILadderChallengeStore ladderChallengeStore = null)
		{
			_ladderStore = ladderStore ?? new LadderStore();
			_ladderChallengeStore = ladderChallengeStore ?? new LadderChallengeStore();
		}

		public Result HandleRequest(SetLadderMatchTimeRequest request, IUser loggedInUser)
		{
			// TODO: validate user can set match time
			_ladderChallengeStore.SetMatchTime(request.LadderChallengeId, request.MatchTime);
			return Result.Successful();
		}

		private readonly ILadderStore _ladderStore;
		private readonly ILadderChallengeStore _ladderChallengeStore;
	}
}
