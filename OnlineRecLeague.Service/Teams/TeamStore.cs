using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.TeamMembers;
using OnlineRecLeague.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Teams
{
	public interface ITeamStore
	{
		bool TryFindTeam(Guid teamId, out ITeam team);
		IReadOnlyList<ITeam> FindTeams(IReadOnlyList<Guid> teamIds);
		ITeam CreateTeam(CreateTeamRequest request, IUser teamCreator);
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
					created_at as createdat
				FROM
					public.team
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

		public ITeam CreateTeam(CreateTeamRequest request, IUser teamCreator)
		{
			const string sql = @"
				INSERT INTO public.team
				(name, owner_user_id, profile_content, user_name_prefix, owner_user_id, created_at)
				VALUES
				(@Name, @OwnerUserId, @ProfileContent, @UserNamePrefix, @OwnerUserId, @CreatedAt)
				RETURNING team_id;";

			using (var connection = AppDataConnection.Create())
			{
				var args = new
					{
						request.Name,
						request.ProfileContent,
						request.UserNamePrefix,
						OwnerUserId = teamCreator.UserId,
						CreatedAt = teamCreator.DefaultTimezone.CurrentTime(),
					};

				var team_id = connection.Query<Guid>(sql, args).Single();
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

		private ITeam CreateTeam(TeamRecord record, Func<IReadOnlyList<ITeamMember>> findTeamMembersFunc)
		{
			return new Team(record.TeamId, findTeamMembersFunc)
				{
					Name = record.Name,
					ProfileContent = record.ProfileContent,
					UserNamePrefix = record.UserNamePrefix,
					OwnerUserId = record.OwnerUserId,
					CreatedAt = record.CreatedTime,
				};
		}
	}
}
