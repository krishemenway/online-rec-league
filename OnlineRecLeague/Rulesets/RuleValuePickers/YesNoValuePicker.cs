namespace OnlineRecLeague.Rulesets.RuleValuePickers
{
	public class YesNoValuePicker : IRuleValuePicker
	{
		public YesNoValuePicker(string defaultValue)
		{
			DefaultValue = defaultValue;
		}

		public string ValuePickerType => nameof(YesNoValuePicker);
		public string DefaultValue { get; private set; }

		public bool IsValid(string value)
		{
			return value.Equals(YesValue) || value.Equals(NoValue);
		}

		public const string YesValue = "Yes";
		public const string NoValue = "No";
	}
}
