using Npgsql;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace OnlineRecLeague.AppData
{
	internal class AppDataConnection
	{
		internal static IDbConnection Create()
		{
			var connection = new NpgsqlConnection(ConnectionString.Value);
			connection.Open();
			return connection;
		}

		private static string CreateConnectionString()
		{
			var connectionParams = new Dictionary<string, string>
				{
					{ "Host", Program.Settings.GetValue<string>("AppDataHost") },
					{ "Port", Program.Settings.GetValue<int>("AppDataPort").ToString() },
					{ "Username", Program.Settings.GetValue<string>("AppDataUser") },
					{ "Password", Program.Settings.GetValue<string>("AppDataPassword") },
					{ "Database", Program.Settings.GetValue<string>("AppDatabaseName") },
				};

			return string.Join(";", connectionParams.Select(param => $"{param.Key}={param.Value}"));
		}

		private static Lazy<string> ConnectionString = new Lazy<string>(() => CreateConnectionString());
	}
}
