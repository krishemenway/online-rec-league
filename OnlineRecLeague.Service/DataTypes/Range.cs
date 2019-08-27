using System;

namespace OnlineRecLeague.DataTypes
{
	internal struct Range<T> where T : IComparable<T>
	{
		internal Range(T start, T end)
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
			return obj is Range<T> objAsRange && this.Equals(objAsRange, (o) => o.Start, (o) => o.End);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Start, End);
		}

		public override string ToString()
		{
			return $"{Start.ToString()} {End.ToString()}";
		}
	}
}
