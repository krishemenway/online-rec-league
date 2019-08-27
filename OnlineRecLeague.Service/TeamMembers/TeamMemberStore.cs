using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.Teams;
using OnlineRecLeague.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.TeamMembers
{
	public interface ITeamMemberStore
	{
		void JoinTeam(IUser user, ITeam team);
		IReadOnlyDictionary<Guid, IReadOnlyList<ITeamMember>> FindTeamMembers(IReadOnlyList<Guid> teamIds);
	}

	public class TeamMemberStore : ITeamMemberStore
	{
		public IReadOnlyDictionary<Guid, IReadOnlyList<ITeamMember>> FindTeamMembers(IReadOnlyList<Guid> teamIds)
		{
			const string sql = @"
				SELECT
					team_member_id as teammemberid,
					team_id as teamid,
					user_id as userid,
					nickname,
					joined_time as joinedtime
				FROM
					svc.team_member
				WHERE
					team_id = any(@TeamIds)";

			using (var connection = AppDataConnection.Create())
			{
				return connection
					.Query<TeamMemberRecord>(sql, new { teamIds })
					.GroupBy(x => x.TeamId, x => x)
					.ToDictionary(x => x.Key, x => (IReadOnlyList<ITeamMember>)x.Select(CreateTeamMember).ToList());
			}
		}

		public void JoinTeam(IUser user, ITeam team)
		{
			const string sql = @"
				INSERT INTO svc.team_member
				(team_id, user_id, nickname, joined_time)
				VALUES
				(@TeamId, @UserId, @NickName, @JoinedTime)";

			using (var connection = AppDataConnection.Create())
			{
				connection.Execute(sql, new { user.UserId, user.NickName, team.TeamId, JoinedTime = user.DefaultTimezone.CurrentTime() });
			}
		}

		private ITeamMember CreateTeamMember(TeamMemberRecord record)
		{
			return new TeamMember(record);
		}
	}
}
