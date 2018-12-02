namespace OnlineRecLeague.Ruleset
{
	public interface IRuleValuePicker
	{
		string ValuePickerType { get; }
		string DefaultValue { get; }

		bool IsValid(string value);
	}
}
