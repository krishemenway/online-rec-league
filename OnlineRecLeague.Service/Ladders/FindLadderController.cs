using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;

namespace OnlineRecLeague.Ladders
{
	[ApiController]
	[Route("api/ladders")]
	public class FindLadderController : ControllerBase
	{
		public FindLadderController(
			ILadderStore ladderStore = null,
			ILadderViewModelFactory ladderViewModelFactory = null)
		{
			_ladderStore = ladderStore ?? new LadderStore();
			_ladderViewModelFactory = ladderViewModelFactory ?? new LadderViewModelFactory();
		}

		[HttpGet(nameof(Find))]
		[ProducesResponseType(200, Type = typeof(Result<LadderViewModel>))]
		public ActionResult<Result<LadderViewModel>> Find([FromQuery] FindLadderRequest request)
		{
			var ladder = _ladderStore.Find(request.LadderId);
			var ladderViewModel = _ladderViewModelFactory.CreateDetailedViewModel(ladder);

			return Result<LadderViewModel>.Successful(ladderViewModel);
		}

		private readonly ILadderStore _ladderStore;
		private readonly ILadderViewModelFactory _ladderViewModelFactory;
	}
}
