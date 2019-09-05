using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.Service.DataTypes;
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
		IReadOnlyDictionary<Id<Team>, IReadOnlyList<ITeamMember>> FindTeamMembers(IReadOnlyList<Id<Team>> teamIds);
	}

	public class TeamMemberStore : ITeamMemberStore
	{
		public IReadOnlyDictionary<Id<Team>, IReadOnlyList<ITeamMember>> FindTeamMembers(IReadOnlyList<Id<Team>> teamIds)
		{
			const string sql = @"
				SELECT
					team_member_id as teammemberid,
					team_id as teamid,
					user_id as userid,
					nickname,
					joined_time as joinedtime
				FROM
					public.team_member
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
				INSERT INTO public.team_member
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

	public class TeamMemberRecord
	{
		public Id<TeamMember> TeamMemberId { get; set; }

		public Id<Team> TeamId { get; set; }
		public Id<User> UserId { get; set; }

		public string NickName { get; set; }
		public DateTime JoinedTime { get; set; }
	}
}
