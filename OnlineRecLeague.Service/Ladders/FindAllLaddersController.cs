using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Ladders
{
	[ApiController]
	[Route("api/ladders")]
	public class FindAllLaddersController : ControllerBase
	{
		public FindAllLaddersController(
			ILadderStore ladderStore = null,
			ILadderViewModelFactory ladderViewModelFactory = null)
		{
			_ladderStore = ladderStore ?? new LadderStore();
			_ladderViewModelFactory = ladderViewModelFactory ?? new LadderViewModelFactory();
		}

		[HttpGet(nameof(All))]
		[ProducesResponseType(200, Type = typeof(IReadOnlyList<LadderViewModel>))]
		public ActionResult<Result<IReadOnlyList<LadderViewModel>>> All()
		{
			var ladders = _ladderStore.FindAll();
			var viewModels = ladders.Select(ladder => _ladderViewModelFactory.CreateBriefViewModel(ladder)).ToList();

			return Result<IReadOnlyList<LadderViewModel>>.Successful(viewModels);
		}

		private readonly ILadderStore _ladderStore;
		private readonly ILadderViewModelFactory _ladderViewModelFactory;
	}
}
