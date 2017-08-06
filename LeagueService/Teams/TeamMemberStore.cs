using Dapper;
using LeagueService.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeagueService.Teams
{
	public interface ITeamMemberStore
	{
		ITeamMember JoinTeam(IUser user, ITeam team);
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
					public.team_member
				WHERE
					team_id = any(@TeamIds)";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<TeamMemberRecord>(sql, new { teamIds }).GroupBy(x => x.TeamId, x => x).ToDictionary(x => x.Key, x => (IReadOnlyList<ITeamMember>)x.Select(CreateTeamMember).ToList());
			}
		}

		public IReadOnlyList<ITeamMember> FindTeamMembers(Guid teamId)
		{
			return new List<ITeamMember>();
		}

		public ITeamMember JoinTeam(IUser user, ITeam team)
		{
			return null;
		}

		private ITeamMember CreateTeamMember(TeamMemberRecord record)
		{
			return new TeamMember
				{
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
