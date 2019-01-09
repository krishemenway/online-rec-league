using OnlineRecLeague.CommonDataTypes;

namespace OnlineRecLeague.Rulesets.RuleValuePickers
{
	public class AgeValuePicker : IRuleValuePicker
	{
		public AgeValuePicker(int defaultAge)
		{
			DefaultValue = defaultAge.ToString();
		}

		public string ValuePickerType => nameof(AgeValuePicker);

		public string DefaultValue { get; private set; }

		public bool IsValid(string value)
		{
			return int.TryParse(value, out var result) && ValidAgeRange.ContainsInclusive(result);
		}

		private static readonly Range<int> ValidAgeRange = new Range<int>(18, 130);
	}
}