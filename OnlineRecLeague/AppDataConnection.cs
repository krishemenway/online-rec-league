using Npgsql;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace OnlineRecLeague
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
			get { return Startup.Configuration.GetValue<string>("OnlineRecLeagueAppDataHost"); }
		}

		public static string Port
		{
			get { return Startup.Configuration.GetValue<string>("OnlineRecLeagueAppDataPort"); }
		}

		public static string User
		{
			get { return Startup.Configuration.GetValue<string>("OnlineRecLeagueAppDataUser"); }
		}

		public static string Password
		{
			get { return Startup.Configuration.GetValue<string>("OnlineRecLeagueAppDataPassword"); }
		}

		public const string DatabaseName = "leagues";
	}
}
