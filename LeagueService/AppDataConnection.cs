using Npgsql;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace LeagueService
{
	public class AppDataConnection
	{
		public static IDbConnection Create()
		{
			var connection = new NpgsqlConnection($"Host={Host};Port={Port};Username={User};Password={Password};Database={DatabaseName}");
			connection.Open();
			return connection;
		}

		public static string Host
		{
			get { return Startup.Configuration.GetValue<string>("LeagueServiceAppDataHost"); }
		}

		public static string Port
		{
			get { return Startup.Configuration.GetValue<string>("LeagueServiceAppDataPort"); }
		}

		public static string User
		{
			get { return Startup.Configuration.GetValue<string>("LeagueServiceAppDataUser"); }
		}

		public static string Password
		{
			get { return Startup.Configuration.GetValue<string>("LeagueServiceAppDataPassword"); }
		}

		public const string DatabaseName = "leagues";
	}
}
