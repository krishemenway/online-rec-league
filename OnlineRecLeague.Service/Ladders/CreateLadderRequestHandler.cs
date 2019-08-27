using OnlineRecLeague.DataTypes;

namespace OnlineRecLeague.Ladders
{
	public interface ICreateLadderRequestHandler
	{
		Result<LadderViewModel> HandleRequest(CreateLadderRequest request);
	}

	internal class CreateLadderRequestHandler : ICreateLadderRequestHandler
	{
		public CreateLadderRequestHandler(
			ILadderStore ladderStore = null,
			ILadderViewModelFactory ladderViewModelFactory = null)
		{
			_ladderStore = ladderStore ?? new LadderStore();
			_ladderViewModelFactory = ladderViewModelFactory ?? new LadderViewModelFactory();
		}

		public Result<LadderViewModel> HandleRequest(CreateLadderRequest request)
		{
			var ladder = _ladderStore.Create(request);
			var viewModel = _ladderViewModelFactory.CreateDetailedViewModel(ladder);

			return Result<LadderViewModel>.Successful(viewModel);
		}

		private readonly ILadderStore _ladderStore;
		private readonly ILadderViewModelFactory _ladderViewModelFactory;
	}
}
