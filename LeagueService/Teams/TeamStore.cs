using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeagueService.Teams
{
	public interface ITeamStore
	{
		bool TryFindTeam(Guid teamId, out ITeam team);
		IReadOnlyList<ITeam> FindTeams(IReadOnlyList<Guid> teamIds);
	}

	public class TeamStore : ITeamStore
	{
		public bool TryFindTeam(Guid teamId, out ITeam team)
		{
			team = FindTeams(new[] { teamId }).SingleOrDefault();
			return team != null;
		}

		public IReadOnlyList<ITeam> FindTeams(IReadOnlyList<Guid> teamIds)
		{
			const string sql = @"
				SELECT
					team_id as teamid,
					name,
					profile_content as profilecontent,
					user_name_prefix as usernameprefix,
					owner_user_id as owneruserid,
					created_time as createdtime
				FROM
					svc.team
				WHERE
					team_id = any(@TeamIds)";

			using (var connection = AppDataConnection.Create())
			{
				var teamMembersByTeamId = new Dictionary<Guid, IReadOnlyList<ITeamMember>>();

				var teams = connection
					.Query<TeamRecord>(sql, new { teamIds })
					.Select(record => CreateTeam(record, () => teamMembersByTeamId[record.TeamId]))
					.ToList();

				LoadTeamMembersForTeams(teams, teamMembersByTeamId);

				return teams;
			}
		}

		public ITeam CreateTeam(UpsertTeamRequest request)
		{
			const string sql = @"
				INSERT INTO svc.team
				(name)
				VALUES
				(@Name)
				RETURNING team_id;";

			using (var connection = AppDataConnection.Create())
			{
				var team_id = connection.Query<Guid>(sql, request).Single();
				return TryFindTeam(team_id, out var team) ? team : throw new Exception("Somehow that team you saved doesn't exist now?");
			}
		}

		private void LoadTeamMembersForTeams(List<ITeam> teams, Dictionary<Guid, IReadOnlyList<ITeamMember>> referenceTeamMembersByTeamId)
		{
			var teamMembersFromDbByTeamId = new TeamMemberStore().FindTeamMembers(teams.Select(x => x.TeamId).ToList());

			foreach(var team in teams)
			{
				referenceTeamMembersByTeamId[team.TeamId] = referenceTeamMembersByTeamId.ContainsKey(team.TeamId) 
					? referenceTeamMembersByTeamId[team.TeamId].ToList() 
					: new List<ITeamMember>();
			}
		}

		public ITeam CreateTeam(TeamRecord record, Func<IReadOnlyList<ITeamMember>> findTeamMembersFunc)
		{
			return new Team(findTeamMembersFunc)
				{
					TeamId = record.TeamId,
					Name = record.Name
				};
		}
	}

	public class UpsertTeamRequest
	{
		public Guid? TeamId { get; set; }
		public string Name { get; set; }
	}

	public class TeamRecord
	{
		public Guid TeamId { get; set; }
		public string Name { get; set; }
		public string ProfileContent { get; set; }
		public string UserNamePrefix { get; set; }
		public Guid OwnerUserId { get; set; }
		public DateTime CreatedTime { get; set; }
	}
}
