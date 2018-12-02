using OnlineRecLeague.CommonDataTypes;
using OnlineRecLeague.Ruleset;
using OnlineRecLeague.Ruleset.RuleValuePickers;
using System.Collections.Generic;

namespace OnlineRecLeague.Ladders
{
	public class LadderRuleMetadataProvider : IRuleMetadataProvider
	{
		public IEnumerable<IRuleMetadata> CreateRuleMetadatas()
		{
			foreach(var rule in new GeneralRuleMetadataProvider().CreateRuleMetadatas())
			{
				yield return rule;
			}

			yield return PlayersAllowedOnTeamRoster;
		}

		internal static readonly RuleMetadata<int> PlayersAllowedOnTeamRoster = new RuleMetadata<int>
			{
				Path = "Ladder/PlayersAllowedOnTeamRoster",
				Name = "Players allowed on team roster",
				ValuePicker = new NumberValuePicker(5, new Range<int>(1, 64)),
			};
	}
}
