namespace OnlineRecLeague.Teams
{
	public interface ITeamProfile
	{
		string Name { get; set; }
	}

	public class TeamProfile : ITeamProfile
	{
		public string Name { get; set; }
	}
}
