using OnlineRecLeague.DataTypes;
using System;

namespace OnlineRecLeague.LadderChallenges
{
	public interface ILadderChallengeRequestHandler
	{
		Result HandleRequest(LadderChallengeRequest request);
	}

	public class LadderChallengeRequestHandler : ILadderChallengeRequestHandler
	{
		public Result HandleRequest(LadderChallengeRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
