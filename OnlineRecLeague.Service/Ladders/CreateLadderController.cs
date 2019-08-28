using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Ladders
{
	[ApiController]
	[Route("api/ladders")]
	[RequiresUserInSession]
	public class CreateLadderController : ControllerBase
	{
		public CreateLadderController(
			ILadderStore ladderStore = null,
			ILadderViewModelFactory ladderViewModelFactory = null)
		{
			_ladderStore = ladderStore ?? new LadderStore();
			_ladderViewModelFactory = ladderViewModelFactory ?? new LadderViewModelFactory();
		}

		[HttpPost(nameof(Create))]
		[ProducesResponseType(200, Type = typeof(Result<LadderViewModel>))]
		public ActionResult<Result<LadderViewModel>> Create([FromBody] CreateLadderRequest request)
		{
			var ladder = _ladderStore.Create(request);
			var viewModel = _ladderViewModelFactory.CreateDetailedViewModel(ladder);

			return Result<LadderViewModel>.Successful(viewModel);
		}

		private readonly ILadderStore _ladderStore;
		private readonly ILadderViewModelFactory _ladderViewModelFactory;
	}
}
