using OnlineRecLeague.DataTypes;

namespace OnlineRecLeague.Ladders
{
	public class FindLadderRequestHandler
	{
		public FindLadderRequestHandler(
			ILadderStore ladderStore = null,
			ILadderViewModelFactory ladderViewModelFactory = null)
		{
			_ladderStore = ladderStore ?? new LadderStore();
			_ladderViewModelFactory = ladderViewModelFactory ?? new LadderViewModelFactory();
		}

		public Result<LadderViewModel> HandleRequest(FindLadderRequest request)
		{
			var ladder = _ladderStore.Find(request.LadderId);
			var ladderViewModel = _ladderViewModelFactory.CreateDetailedViewModel(ladder);
			return Result<LadderViewModel>.Successful(ladderViewModel);
		}

		private readonly ILadderStore _ladderStore;
		private readonly ILadderViewModelFactory _ladderViewModelFactory;
	}
}
