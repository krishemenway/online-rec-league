using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using System;

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
			if (!TryFindLadder(request, out var ladder))
			{
				return Result<LadderViewModel>.Failure("Could not find ladder.");
			}

			var ladderViewModel = _ladderViewModelFactory.CreateDetailedViewModel(ladder);
			return Result<LadderViewModel>.Successful(ladderViewModel);
		}

		private bool TryFindLadder(FindLadderRequest request, out ILadder ladder)
		{
			if (request.LadderId.HasValue)
			{
				ladder = _ladderStore.Find(request.LadderId.Value);
				return true;
			}

			if (!string.IsNullOrEmpty(request.Path))
			{
				ladder = _ladderStore.FindByPath(request.Path);
				return true;
			}

			ladder = null;
			return false;
		}

		private readonly ILadderStore _ladderStore;
		private readonly ILadderViewModelFactory _ladderViewModelFactory;
	}

	public class FindLadderRequest
	{
		public Guid? LadderId { get; set; }
		public string Path { get; set; }
	}
}
