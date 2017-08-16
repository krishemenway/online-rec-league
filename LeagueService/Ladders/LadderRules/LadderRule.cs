using Newtonsoft.Json;
using System;

namespace LeagueService.Ladders.LadderRules
{
	public interface ILadderRule
	{
		LadderRuleType RuleType { get; }
		string RuleValue { get; }

		int RuleValueAsInt { get; }
	}

	public class LadderRule : ILadderRule
	{
		public LadderRuleType RuleType { get; set; }
		public string RuleValue { get; set; }

		[JsonIgnore]
		public int RuleValueAsInt => Convert.ToInt32(RuleValue);

		[JsonIgnore]
		public bool RuleValueAsBool => Convert.ToBoolean(RuleValue);
	}

	public enum LadderRuleType
	{
		ForceRealNames
	}
}
