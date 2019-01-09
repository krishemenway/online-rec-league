using System;

namespace OnlineRecLeague.CommonDataTypes
{
	public struct Range<T> where T : IComparable<T>
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

		public override bool Equals(object obj)
		{
			return obj is Range<T> objAsRange && Start.Equals(objAsRange.Start) && End.Equals(objAsRange.End);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = (int)2166136261;
				hash = (hash * 16777619) ^ Start.GetHashCode();
				hash = (hash * 16777619) ^ End.GetHashCode();
				return hash;
			}
		}

		public override string ToString()
		{
			return $"{Start.ToString()} {End.ToString()}";
		}
	}
}
