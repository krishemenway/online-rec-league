using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;
using System;

namespace OnlineRecLeague.Ladders
{
	[ApiController]
	[Route("api/ladders")]
	[RequiresUserInSession]
	public class CreateLadderController : ControllerBase
	{
		public CreateLadderController(
			ILadderStore ladderStore = null,
			ILadderViewModelFactory ladderViewModelFactory = null,
			IUserSessionStore userSessionStore = null)
		{
			_ladderStore = ladderStore ?? new LadderStore();
			_ladderViewModelFactory = ladderViewModelFactory ?? new LadderViewModelFactory();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
		}

		[HttpPost(nameof(Create))]
		[ProducesResponseType(200, Type = typeof(Result<LadderViewModel>))]
		public ActionResult<Result<LadderViewModel>> Create([FromBody] CreateLadderRequest request)
		{
			var user = _userSessionStore.FindUserOrThrow(HttpContext.Session);
			var ladder = _ladderStore.Create(request, user);
			var viewModel = _ladderViewModelFactory.CreateDetailedViewModel(ladder);

			return Result<LadderViewModel>.Successful(viewModel);
		}

		private readonly ILadderStore _ladderStore;
		private readonly ILadderViewModelFactory _ladderViewModelFactory;
		private readonly IUserSessionStore _userSessionStore;
	}

	public class CreateLadderRequest
	{
		public string Name { get; set; }
		public string UriPath { get; set; }
		public Guid SportId { get; set; }
		public string Rules { get; set; }
	}
}
