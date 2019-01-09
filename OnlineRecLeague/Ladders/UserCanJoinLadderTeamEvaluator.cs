using OnlineRecLeague.CommonDataTypes;
using OnlineRecLeague.Rulesets;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Ladders
{
	public interface IUserCanJoinLadderTeamEvaluator
	{
		Result Evaluate(ILadder ladder, IUser user);
	}

	public class UserCanJoinLadderTeamEvaluator : IUserCanJoinLadderTeamEvaluator
	{
		public Result Evaluate(ILadder ladder, IUser user)
		{
			if (GeneralRuleMetadataProvider.ForceRealNamesRuleMetadata.TryGetRuleValue(ladder.Rules, out var forceRealNames) && forceRealNames)
			{
				if (!string.IsNullOrEmpty(user.RealName))
				{
					return Result.Failure("User requires a real name to join");
				}
			}

			return Result.Successful();
		}
	}
}
