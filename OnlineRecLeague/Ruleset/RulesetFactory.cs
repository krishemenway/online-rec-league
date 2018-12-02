using Newtonsoft.Json;
using System.Collections.Generic;

namespace OnlineRecLeague.Ladders.LadderRules
{
	public interface IRulesetFactory
	{
		IRuleset Create(string rulesAsJson);
	}

	public class RulesetFactory : IRulesetFactory
	{
		public IRuleset Create(string rulesAsJson)
		{
			return new Ruleset(JsonConvert.DeserializeObject<Dictionary<string, string>>(rulesAsJson));
		}
	}
}
