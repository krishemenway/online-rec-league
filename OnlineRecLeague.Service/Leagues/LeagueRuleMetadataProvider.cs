using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Rulesets;
using OnlineRecLeague.Rulesets.RuleValuePickers;
using System.Collections.Generic;

namespace OnlineRecLeague.Leagues
{
	internal class LeagueRuleMetadataProvider : IRuleMetadataProvider
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
				Path = "League/PlayersAllowedOnTeamRoster",
				Name = "Players allowed on team roster",
				ValuePicker = new NumberValuePicker(5, new Range<int>(1, 64)),
			};
	}
}
