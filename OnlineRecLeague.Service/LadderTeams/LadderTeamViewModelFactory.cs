namespace OnlineRecLeague.LadderTeams
{
	public interface ILadderTeamViewModelFactory
	{
		LadderTeamViewModel Create(ILadderTeam ladderTeam);
	}

	internal class LadderTeamViewModelFactory : ILadderTeamViewModelFactory
	{
		public LadderTeamViewModel Create(ILadderTeam ladderTeam)
		{
			return new LadderTeamViewModel
				{
					TeamId = ladderTeam.Team.TeamId,
					Name = ladderTeam.Team.Name,
					Rung = ladderTeam.Rung,
				};
		}
	}
}
