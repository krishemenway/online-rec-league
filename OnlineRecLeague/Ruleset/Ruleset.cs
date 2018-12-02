using System;
using System.Collections.Generic;

namespace OnlineRecLeague.Ladders.LadderRules
{
	public interface IRuleset
	{
		bool TryGet<T>(string path, out T value);
	}

	public class Ruleset : IRuleset
	{
		public Ruleset(Dictionary<string, string> rulesWithValues = null)
		{
			_rulesWithValues = rulesWithValues ?? new Dictionary<string, string>();
		}

		public bool TryGet<T>(string path, out T value)
		{
			try
			{
				if (!_rulesWithValues.TryGetValue(path, out var valueAsString))
				{
					value = default(T);
					return false;
				}

				value = (T)Convert.ChangeType(valueAsString, typeof(T));
				return true;
			}
			catch (Exception)
			{
				value = default(T);
				return false;
			}
		}

		private readonly Dictionary<string, string> _rulesWithValues;
	}
}
