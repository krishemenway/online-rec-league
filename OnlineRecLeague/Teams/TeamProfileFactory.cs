using System;

namespace OnlineRecLeague.Teams
{
	public interface ITeamProfile
	{
	}

	public interface ITeamProfileFactory
	{
		ITeamProfile Create(ITeam team);
	}

	public class TeamProfileFactory : ITeamProfileFactory
	{
		public ITeamProfile Create(ITeam team)
		{
			throw new NotImplementedException();
		}
	}
}
