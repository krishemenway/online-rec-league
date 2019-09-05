using OnlineRecLeague.Service.DataTypes;
using OnlineRecLeague.TeamMembers;
using OnlineRecLeague.Users;
using System;
using System.Collections.Generic;

namespace OnlineRecLeague.Teams
{
	public interface ITeam
	{
		Id<Team> TeamId { get; }
		string Name { get; }

		DateTimeOffset CreatedTime { get; }
		Id<User> OwnerUserId { get; }

		string ProfileContent { get; }

		IReadOnlyList<ITeamMember> TeamMembers { get; }
	}

	public class Team : ITeam
	{
		public Team(Id<Team> teamId, Func<IReadOnlyList<ITeamMember>> findTeamMembersFunc = null)
		{
			TeamId = teamId;
			_lazyTeamMembers = new Lazy<IReadOnlyList<ITeamMember>>(findTeamMembersFunc ?? (() => new TeamMemberStore().FindTeamMembers(new[]{TeamId})[TeamId]));
		}

		public Id<Team> TeamId { get; }
		public string Name { get; set; }

		public DateTimeOffset CreatedTime { get; set; }
		public Id<User> OwnerUserId { get; set; }

		public IReadOnlyList<ITeamMember> TeamMembers => _lazyTeamMembers.Value;

		public string ProfileContent { get; set; }

		public override bool Equals(object other)
		{
			return other is Team otherTeam && TeamId.Equals(otherTeam.TeamId);
		}

		public override int GetHashCode()
		{
			return TeamId.GetHashCode();
		}

		private Lazy<IReadOnlyList<ITeamMember>> _lazyTeamMembers;
	}
}
