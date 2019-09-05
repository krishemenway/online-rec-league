using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace OnlineRecLeague.Service.DataTypes
{
	internal class IdTypeConverter : TypeConverter
	{
		public IdTypeConverter(Type type)
		{
			_typeConstructorInfo = type.GetConstructor(new[] { typeof(Guid) });
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return _typeConstructorInfo.Invoke(new object[] { Guid.Parse((string)value) });
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(Guid);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return ((IId)value).Value.ToString();
			}

			if (destinationType == typeof(Guid))
			{
				return ((IId)value).Value;
			}

			throw new NotSupportedException($"Cannot convert to type {destinationType}");
		}

		private readonly ConstructorInfo _typeConstructorInfo;
	}
}
