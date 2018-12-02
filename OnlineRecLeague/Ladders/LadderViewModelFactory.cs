using OnlineRecLeague.Ladders.LadderTeams;
using System.Linq;

namespace OnlineRecLeague.Ladders
{
	public interface ILadderViewModelFactory
	{
		LadderViewModel CreateBriefViewModel(ILadder ladder);
		LadderViewModel CreateDetailedViewModel(ILadder ladder);
	}

	internal class LadderViewModelFactory : ILadderViewModelFactory
	{
		public LadderViewModelFactory(ILadderTeamViewModelFactory ladderTeamViewModelFactory = null)
		{
			_ladderTeamViewModelFactory = ladderTeamViewModelFactory ?? new LadderTeamViewModelFactory();
		}

		public LadderViewModel CreateBriefViewModel(ILadder ladder)
		{
			return new LadderViewModel
				{
					LadderId = ladder.LadderId,
					Name = ladder.Name,
				};
		}

		public LadderViewModel CreateDetailedViewModel(ILadder ladder)
		{
			return new LadderViewModel
				{
					LadderId = ladder.LadderId,
					Name = ladder.Name,

					AllTeams = ladder.AllLadderTeams
						.OrderBy(ladderTeam => ladderTeam.Rung)
						.Select(ladderTeam => _ladderTeamViewModelFactory.Create(ladderTeam))
						.ToList(),
				};
		}

		private readonly ILadderTeamViewModelFactory _ladderTeamViewModelFactory;
	}
}
