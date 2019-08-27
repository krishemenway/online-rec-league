using OnlineRecLeague.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Ladders
{
	public interface IFindAllLaddersRequestHandler
	{
		Result<IReadOnlyList<LadderViewModel>> HandleRequest();
	}

	internal class FindAllLaddersRequestHandler : IFindAllLaddersRequestHandler
	{
		public FindAllLaddersRequestHandler(
			ILadderStore ladderStore = null,
			ILadderViewModelFactory ladderViewModelFactory = null)
		{
			_ladderStore = ladderStore ?? new LadderStore();
			_ladderViewModelFactory = ladderViewModelFactory ?? new LadderViewModelFactory();
		}

		public Result<IReadOnlyList<LadderViewModel>> HandleRequest()
		{
			var ladders = _ladderStore.FindAll();
			var viewModels = ladders.Select(ladder => _ladderViewModelFactory.CreateBriefViewModel(ladder)).ToList();

			return Result<IReadOnlyList<LadderViewModel>>.Successful(viewModels);
		}

		private readonly ILadderStore _ladderStore;
		private readonly ILadderViewModelFactory _ladderViewModelFactory;
	}
}
