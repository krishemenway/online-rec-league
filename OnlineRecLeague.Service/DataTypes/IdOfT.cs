using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OnlineRecLeague.Service.DataTypes
{
	public interface IId
	{
		Guid Value { get; }
	}

	[TypeConverter(typeof(IdTypeConverter))]
	public struct Id<T> : IId, IEquatable<Id<T>>, IComparable<Id<T>>
	{
		public Id(Guid value)
		{
			Value = value;
		}

		public static Id<T> NewId()
		{
			return new Id<T>(Guid.NewGuid());
		}

		public static bool TryParse(string value, out Id<T> id)
		{
			if (Guid.TryParse(value, out var parsedGuid))
			{
				id = (Id<T>)parsedGuid;
				return true;
			}
			else
			{
				id = Empty;
				return false;
			}
		}

		public override bool Equals(object otherObj)
		{
			return otherObj is Id<T> otherId && Value.Equals(otherId.Value);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public bool Equals(Id<T> other)
		{
			return Value.Equals(other.Value);
		}

		public int CompareTo(Id<T> other)
		{
			return Value.CompareTo(other.Value);
		}

		public static Id<T> Empty { get; } = new Id<T>(Guid.Empty);

		public static explicit operator Guid(Id<T> id)
		{
			return id.Value;
		}

		public static explicit operator Id<T>(Guid guid)
		{
			return new Id<T>(guid);
		}

		public static explicit operator Id<T>(string guid)
		{
			return new Id<T>(Guid.Parse(guid));
		}

		public static bool operator ==(Id<T> a, Id<T> b)
		{
			return a.Value.Equals(b.Value);
		}

		public static bool operator !=(Id<T> a, Id<T> b)
		{
			return !a.Value.Equals(b.Value);
		}

		public Guid Value { get; set; }
	}

	public static class ListOfIdOfTExtensions
	{
		public static IEnumerable<Guid> ConvertToGuids<T>(this IEnumerable<Id<T>> collectionOfIds)
		{
			return collectionOfIds.Select(x => x.Value);
		}
	}
}
