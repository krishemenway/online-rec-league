using System.Collections.Generic;

namespace OnlineRecLeague.Rulesets
{
	public interface IRuleMetadataProvider
	{
		IEnumerable<IRuleMetadata> CreateRuleMetadatas();
	}
}
