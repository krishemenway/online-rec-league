using System;

namespace OnlineRecLeague.CommonDataTypes
{
	public class Range<T> where T : IComparable<T>
	{
		public Range(T start, T end)
		{
			Start = start;
			End = end;
		}

		public T Start { get; private set; }
		public T End { get; private set; }

		internal bool ContainsInclusive(T value)
		{
			return Start.CompareTo(value) <= 0 && End.CompareTo(value) >= 0;
		}
	}
}
