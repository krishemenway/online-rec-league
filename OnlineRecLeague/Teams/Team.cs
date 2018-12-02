using System;
using System.Collections.Generic;

namespace OnlineRecLeague.Teams
{
	public interface ITeam
	{
		Guid TeamId { get; }
		string Name { get; }

		IReadOnlyList<ITeamMember> TeamMembers { get; }
	}

	public class Team : ITeam
	{
		public Team(Guid teamId, Func<IReadOnlyList<ITeamMember>> findTeamMembersFunc = null)
		{
			TeamId = teamId;
			_lazyTeamMembers = new Lazy<IReadOnlyList<ITeamMember>>(findTeamMembersFunc ?? (() => new TeamMemberStore().FindTeamMembers(new[]{TeamId})[TeamId]));
		}

		public Guid TeamId { get; }
		public string Name { get; set; }

		public IReadOnlyList<ITeamMember> TeamMembers => _lazyTeamMembers.Value;

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
