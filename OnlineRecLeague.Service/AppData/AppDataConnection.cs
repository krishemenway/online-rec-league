using Npgsql;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using OnlineRecLeague.Service.DataTypes;
using OnlineRecLeague.Leagues;
using OnlineRecLeague.LeagueMatches;
using OnlineRecLeague.Games;
using OnlineRecLeague.LeagueTeams;
using OnlineRecLeague.TeamMembers;
using OnlineRecLeague.Teams;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.AppData
{
	internal class AppDataConnection
	{
		static AppDataConnection()
		{
			Dapper.SqlMapper.AddTypeHandler(new IdDapperConverter<Game>());
			Dapper.SqlMapper.AddTypeHandler(new IdDapperConverter<League>());
			Dapper.SqlMapper.AddTypeHandler(new IdDapperConverter<LeagueTeam>());
			Dapper.SqlMapper.AddTypeHandler(new IdDapperConverter<LeagueMatch>());
			Dapper.SqlMapper.AddTypeHandler(new IdDapperConverter<Team>());
			Dapper.SqlMapper.AddTypeHandler(new IdDapperConverter<TeamMember>());
			Dapper.SqlMapper.AddTypeHandler(new IdDapperConverter<User>());
		}

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

		private static readonly Lazy<string> ConnectionString = new Lazy<string>(() => CreateConnectionString());
	}
}
