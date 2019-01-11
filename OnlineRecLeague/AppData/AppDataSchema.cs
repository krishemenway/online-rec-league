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

		// List should be ordered with dependencies in mind
		public static IReadOnlyDictionary<string, string> Tables = new Dictionary<string, string>
			{
				{ "svc.game", "./Games/Game.sql" },
				{ "svc.user", "./Users/User.sql" },
				{ "svc.team", "./Teams/Team.sql" },
				{ "svc.team_member", "./TeamMembers/TeamMember.sql" },
				{ "svc.invite_to_team", "./TeamMembers/InviteToTeam.sql" },
				{ "svc.ladder", "./Ladders/Ladder.sql" },
				{ "svc.ladder_challenge", "./LadderChallenges/LadderChallenge.sql" },
				{ "svc.ladder_team", "./LadderTeams/LadderTeam.sql" },
				{ "svc.league", "./Leagues/League.sql" },
			};
	}
}
