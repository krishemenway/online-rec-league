using OnlineRecLeague.Rulesets.RuleValuePickers;
using System.Collections.Generic;

namespace OnlineRecLeague.Rulesets
{
	public class GeneralRuleMetadataProvider : IRuleMetadataProvider
	{
		public IEnumerable<IRuleMetadata> CreateRuleMetadatas()
		{
			yield return MinimumAgeToJoinRuleMetadata;
			yield return ForceRealNamesRuleMetadata;
		}

		internal static readonly RuleMetadata<int> MinimumAgeToJoinRuleMetadata = new RuleMetadata<int>
			{
				Path = "General/MinimumAgeToJoin",
				Name = "Minimum Player Age to Join",
				ValuePicker = new AgeValuePicker(18),
			};

		internal static readonly RuleMetadata<bool> ForceRealNamesRuleMetadata = new RuleMetadata<bool>
			{
				Path = "General/ForceRealNames",
				Name = "Real Names Only",
				ValuePicker = new YesNoValuePicker(YesNoValuePicker.NoValue),
			};
	}
}
