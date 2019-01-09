namespace OnlineRecLeague.Rulesets
{
	public interface IRuleValuePicker
	{
		string ValuePickerType { get; }
		string DefaultValue { get; }

		bool IsValid(string value);
	}
}
