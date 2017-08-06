using System;
using LeagueService.Users;

namespace LeagueService.Teams
{
	public interface ITeamMember
	{
		IUser User { get; }
		ITeam Team { get; }

		string NickName { get; }
		DateTime JoinedTime { get; }
	}

	public class TeamMember : ITeamMember
	{
		public IUser User { get; set; }
		public ITeam Team { get; set; }

		public string NickName { get; set; }
		public DateTime JoinedTime { get; set; }
	}
}
