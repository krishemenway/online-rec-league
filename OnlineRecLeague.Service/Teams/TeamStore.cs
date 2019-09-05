using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.Service.DataTypes;
using OnlineRecLeague.TeamMembers;
using OnlineRecLeague.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Teams
{
	public interface ITeamStore
	{
		bool TryFindTeam(Id<Team> teamId, out ITeam team);
		IReadOnlyList<ITeam> FindTeams(IReadOnlyList<Id<Team>> teamIds);
		ITeam CreateTeam(CreateTeamRequest request, IUser teamCreator);
	}

	public class TeamStore : ITeamStore
	{
		public bool TryFindTeam(Id<Team> teamId, out ITeam team)
		{
			team = FindTeams(new[] { teamId }).SingleOrDefault();
			return team != null;
		}

		public IReadOnlyList<ITeam> FindTeams(IReadOnlyList<Id<Team>> teamIds)
		{
			const string sql = @"
				SELECT
					team_id as teamid,
					name,
					profile_content as profilecontent,
					owner_user_id as owneruserid,
					created_time as createdtime
				FROM
					public.team
				WHERE
					team_id = any(@TeamIds)";

			using (var connection = AppDataConnection.Create())
			{
				var teamMembersByTeamId = new Dictionary<Id<Team>, IReadOnlyList<ITeamMember>>();

				var teams = connection
					.Query<TeamRecord>(sql, new { teamIds })
					.Select(record => CreateTeam(record, () => teamMembersByTeamId[record.TeamId]))
					.ToList();

				LoadTeamMembersForTeams(teams, teamMembersByTeamId);

				return teams;
			}
		}

		public ITeam CreateTeam(CreateTeamRequest request, IUser teamCreator)
		{
			const string sql = @"
				INSERT INTO public.team
				(name, owner_user_id, profile_content, owner_user_id, created_time)
				VALUES
				(@Name, @OwnerUserId, @ProfileContent, @OwnerUserId, @CreatedTime)
				RETURNING team_id;";

			using (var connection = AppDataConnection.Create())
			{
				var args = new
				{
					request.Name,
					request.ProfileContent,
					OwnerUserId = teamCreator.UserId,
					CreatedAt = teamCreator.DefaultTimezone.CurrentTime(),
				};

				var teamId = connection.Query<Id<Team>>(sql, args).Single();
				return TryFindTeam(teamId, out var team) ? team : throw new Exception("Somehow that team you saved doesn't exist now?");
			}
		}

		private void LoadTeamMembersForTeams(List<ITeam> teams, Dictionary<Id<Team>, IReadOnlyList<ITeamMember>> referenceTeamMembersByTeamId)
		{
			var teamMembersFromDbByTeamId = new TeamMemberStore().FindTeamMembers(teams.Select(x => x.TeamId).ToList());

			foreach(var team in teams)
			{
				referenceTeamMembersByTeamId[team.TeamId] = referenceTeamMembersByTeamId.ContainsKey(team.TeamId) 
					? referenceTeamMembersByTeamId[team.TeamId].ToList() 
					: new List<ITeamMember>();
			}
		}

		private ITeam CreateTeam(TeamRecord record, Func<IReadOnlyList<ITeamMember>> findTeamMembersFunc)
		{
			return new Team(record.TeamId, findTeamMembersFunc)
			{
				Name = record.Name,
				ProfileContent = record.ProfileContent,
				OwnerUserId = record.OwnerUserId,
				CreatedTime = record.CreatedTime,
			};
		}
	}

	public class TeamRecord
	{
		public Id<Team> TeamId { get; set; }
		public string Name { get; set; }
		public string ProfileContent { get; set; }
		public Id<User> OwnerUserId { get; set; }
		public DateTime CreatedTime { get; set; }
	}
}
