using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OnlineRecLeague.AppData
{
	public class AppDataSchema
	{
		public static string CreateSchemaScript()
		{
			return $@"
				{string.Join("\n", Tables.Select(x => $"DROP TABLE IF EXISTS {x.Key};"))}
				{string.Join("\n\n", Tables.Reverse().Select(x => GetSchemaContentFromFile(x.Value)))}";
		}

		private static string GetSchemaContentFromFile(string path)
		{
			using(var streamReader = new StreamReader(path))
			{
				return streamReader.ReadToEnd();
			}
		}

		public static IReadOnlyDictionary<string, string> Tables = new Dictionary<string, string>
			{
				// { "svc.league", "./Leagues/League.sql" },
				{ "svc.invite_to_team", "./Teams/InviteToTeam.sql" },
				{ "svc.team_member", "./Teams/TeamMember.sql" },
				{ "svc.team", "./Teams/Team.sql" },
				{ "svc.user", "./Users/User.sql" },
			};
	}
}
