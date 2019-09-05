using System;
using OnlineRecLeague.Service.DataTypes;
using OnlineRecLeague.Teams;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.TeamMembers
{
	public interface ITeamMember
	{
		IUser User { get; }
		ITeam Team { get; }

		string NickName { get; }
		DateTimeOffset JoinedTime { get; }
	}

	public class TeamMember : ITeamMember
	{
		public TeamMember(TeamMemberRecord record)
		{
			TeamMemberId = record.TeamMemberId;
			TeamId = record.TeamId;
			UserId = record.UserId;
			NickName = record.NickName;
			JoinedTime = record.JoinedTime;
		}

		public Id<TeamMember> TeamMemberId { get; }

		public IUser User { get; set; }
		public ITeam Team { get; set; }

		public Id<Team> TeamId { get; set; }
		public Id<User> UserId { get; set; }

		public string NickName { get; set; }
		public DateTimeOffset JoinedTime { get; set; }

		public override bool Equals(object other)
		{
			return other is TeamMember otherTeamMember && TeamMemberId.Equals(otherTeamMember.TeamMemberId);
		}

		public override int GetHashCode()
		{
			return TeamMemberId.GetHashCode();
		}
	}
}
