namespace OnlineRecLeague.Teams
{
	public interface ITeamProfileFactory
	{
		ITeamProfile Create(ITeam team);
	}

	internal class TeamProfileFactory : ITeamProfileFactory
	{
		public ITeamProfile Create(ITeam team)
		{
			return new TeamProfile
				{
					Name = team.Name,
				};
		}
	}
}
