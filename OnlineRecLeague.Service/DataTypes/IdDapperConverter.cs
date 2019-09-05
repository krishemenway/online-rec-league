using Dapper;
using System;
using System.Data;

namespace OnlineRecLeague.Service.DataTypes
{
	public class IdDapperConverter<T> : SqlMapper.TypeHandler<Id<T>>
	{
		public override Id<T> Parse(object value)
		{
			return new Id<T>((Guid)value);
		}

		public override void SetValue(IDbDataParameter parameter, Id<T> value)
		{
			parameter.DbType = DbType.Guid;
			parameter.Value = value.Value;
		}
	}
}
