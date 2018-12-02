using System.Collections.Generic;

namespace OnlineRecLeague.Ruleset
{
	public interface IRuleMetadataProvider
	{
		IEnumerable<IRuleMetadata> CreateRuleMetadatas();
	}
}
