using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OnlineRecLeague.AppData
{
	internal class AppDataSchema
	{
		public static string CreateSchemaScript()
		{
			return $@"
{RenderDropStatementForEachTable()}
{RenderCreateStatementsForEachTable()}";
		}

		private static object RenderCreateStatementsForEachTable()
		{
			return string.Join("\n\n", Tables.Reverse().Select(x => GetSchemaContentFromFile(x.Value)));
		}

		private static string RenderDropStatementForEachTable()
		{
			return string.Join("\n", Tables.Select(x => $"DROP TABLE IF EXISTS {x.Key};"));
		}

		private static string GetSchemaContentFromFile(string path)
		{
			using(var streamReader = new StreamReader(path))
			{
				return streamReader.ReadToEnd();
			}
		}

		// List should be ordered with dependencies in mind
		public static IReadOnlyDictionary<string, string> Tables = new Dictionary<string, string>
			{
				{ "public.game", "./Games/Game.sql" },
				{ "public.user", "./Users/User.sql" },
				{ "public.team", "./Teams/Team.sql" },
				{ "public.team_member", "./TeamMembers/TeamMember.sql" },
				{ "public.invite_to_team", "./TeamMembers/InviteToTeam.sql" },
				{ "public.league", "./Leagues/League.sql" },
				{ "public.league_match", "./LeagueMatches/LeagueMatch.sql" },
				{ "public.league_team", "./LeagueTeams/LeagueTeam.sql" },
			};
	}
}
