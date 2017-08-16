using Dapper;
using LeagueService.CoreExtensions;
using LeagueService.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeagueService.Teams
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
					team_id as teamid,	Try to get player stats from matches to provide cool data back
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
			return new TeamMember
				{
					TeamId = record.TeamId,
					UserId = record.UserId,
					NickName = record.NickName,
					JoinedTime = record.JoinedTime
				};
		}
	}

	public class TeamMemberRecord
	{
		public Guid TeamMemberId { get; set; }

		public Guid TeamId { get; set; }
		public Guid UserId { get; set; }

		public string NickName { get; set; }
		public DateTime JoinedTime { get; set; }
	}
}
