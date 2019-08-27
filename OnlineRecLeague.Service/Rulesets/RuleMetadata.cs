namespace OnlineRecLeague.Rulesets
{
	public interface IRuleMetadata
	{
		string Path { get; }

		string Name { get; }

		IRuleValuePicker ValuePicker { get; }
	}

	public class RuleMetadata<TValue> : IRuleMetadata
	{
		public string Path { get; internal set; }

		public string Name { get; internal set; }

		public IRuleValuePicker ValuePicker { get; internal set; }

		public bool TryGetRuleValue(IRuleset ruleset, out TValue value)
		{
			return ruleset.TryGet(Path, out value);
		}

		public override bool Equals(object other)
		{
			return other is RuleMetadata<TValue> otherRuleMetadata && Path.Equals(otherRuleMetadata.Path);
		}

		public override int GetHashCode()
		{
			return Path.GetHashCode();
		}
	}
}