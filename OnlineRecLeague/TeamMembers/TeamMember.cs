using System;
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
		public TeamMember(Guid teamMemberId)
		{
			TeamMemberId = teamMemberId;
		}

		public Guid TeamMemberId { get; }

		public IUser User { get; set; }
		public ITeam Team { get; set; }

		public Guid TeamId { get; set; }
		public Guid UserId { get; set; }

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
