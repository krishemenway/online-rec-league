using LeagueService.CommonDataTypes;
using System.Collections.Generic;
using System.Linq;

namespace LeagueService.Ladders
{
	public interface ILadderRepository
	{
		Result<IReadOnlyList<LadderViewModel>> FindAll();

		Result<LadderViewModel> Create(CreateLadderRequest request);
	}

	public class LadderRepository : ILadderRepository
	{
		public LadderRepository(ILadderStore ladderStore = null)
		{
			_ladderStore = ladderStore ?? new LadderStore();
		}

		public Result<IReadOnlyList<LadderViewModel>> FindAll()
		{
			var ladders = _ladderStore.FindAll();
			var viewModels = ladders.Select(CreateViewModel).ToList();

			return Result<IReadOnlyList<LadderViewModel>>.Successful(viewModels);
		}

		public Result<LadderViewModel> Create(CreateLadderRequest request)
		{
			var ladder = _ladderStore.Create(request);
			var viewModel = CreateViewModel(ladder);

			return Result<LadderViewModel>.Successful(viewModel);
		}

		private LadderViewModel CreateViewModel(ILadder ladder)
		{
			return new LadderViewModel
				{
					Name = ladder.Name
				};
		}

		private readonly ILadderStore _ladderStore;
	}

	public class LadderViewModel
	{
		public string Name { get; set; }
	}
}
