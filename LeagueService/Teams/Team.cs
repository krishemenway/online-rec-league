using System;
using System.Collections.Generic;

namespace LeagueService.Teams
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
			_lazyTeamMembers = new Lazy<IReadOnlyList<ITeamMember>>(findTeamMembersFunc ?? (() => new TeamMemberStore().FindTeamMembers(TeamId)));
		}

		public Guid TeamId { get; set; }
		public string Name { get; set; }

		public IReadOnlyList<ITeamMember> TeamMembers => _lazyTeamMembers.Value;

		private Lazy<IReadOnlyList<ITeamMember>> _lazyTeamMembers;
	}
}
