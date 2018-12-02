using OnlineRecLeague.CommonDataTypes;
using OnlineRecLeague.Ruleset;
using OnlineRecLeague.Ruleset.RuleValuePickers;
using System.Collections.Generic;

namespace OnlineRecLeague.Matchups
{
	public class MatchupRuleMetadataProvider : IRuleMetadataProvider
	{
		public IEnumerable<IRuleMetadata> CreateRuleMetadatas()
		{
			yield return MinimumAgeToJoinRuleMetadata;
		}

		internal static readonly RuleMetadata<int> MinimumAgeToJoinRuleMetadata = new RuleMetadata<int>
			{
				Path = "Matchup/PlayerPerTeam",
				Name = "Players per side",
				ValuePicker = new NumberValuePicker(5, new Range<int>(0, 64)),
			};
	}
}
