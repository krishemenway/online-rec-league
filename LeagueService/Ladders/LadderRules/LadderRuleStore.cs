using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace LeagueService.Ladders.LadderRules
{
	public interface ILadderRuleStore
	{
		ILadderRules Create(LadderRecord ladderRecord);
	}

	public class LadderRuleStore : ILadderRuleStore
	{
		public ILadderRules Create(LadderRecord ladderRecord)
		{
			return new LadderRules(JsonConvert.DeserializeObject<IReadOnlyList<LadderRule>>(ladderRecord.Rules));
		}
	}

	public interface ILadderRules
	{
		ILadderRule this[LadderRuleType type] { get; }
	}

	public class LadderRules : ILadderRules
	{
		public LadderRules(IReadOnlyList<ILadderRule> ladderRules)
		{
			_ladderRulesByLadderRuleType = ladderRules.ToDictionary(x => x.RuleType, x => x);
		}

		public ILadderRule this[LadderRuleType type] => _ladderRulesByLadderRuleType[type];

		private readonly Dictionary<LadderRuleType, ILadderRule> _ladderRulesByLadderRuleType;
	}
}
