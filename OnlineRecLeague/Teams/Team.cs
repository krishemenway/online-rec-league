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
		public Team(Func<IReadOnlyList<ITeamMember>> findTeamMembersFunc = null)
		{
			_lazyTeamMembers = new Lazy<IReadOnlyList<ITeamMember>>(findTeamMembersFunc ?? (() => new TeamMemberStore().FindTeamMembers(new[]{TeamId})[TeamId]));
		}

		public Guid TeamId { get; set; }
		public string Name { get; set; }

		public IReadOnlyList<ITeamMember> TeamMembers => _lazyTeamMembers.Value;

		public override bool Equals(object obj)
		{
			var objAsTeam = obj as ITeam;
			return objAsTeam != null && TeamId.Equals(objAsTeam.TeamId);
		}

		public override int GetHashCode()
		{
			return TeamId.GetHashCode();
		}

		private Lazy<IReadOnlyList<ITeamMember>> _lazyTeamMembers;
	}
}
