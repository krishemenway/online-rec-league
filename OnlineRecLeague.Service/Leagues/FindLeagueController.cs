using Microsoft.AspNetCore.Mvc;
using OnlineRecLeague.DataTypes;
using System;

namespace OnlineRecLeague.Leagues
{
	[ApiController]
	[Route("api/leagues")]
	public class FindLeagueController : ControllerBase
	{
		public FindLeagueController(
			ILeagueStore leagueStore = null,
			ILeagueViewModelFactory leagueViewModelFactory = null)
		{
			_leagueStore = leagueStore ?? new LeagueStore();
			_leagueViewModelFactory = leagueViewModelFactory ?? new LeagueViewModelFactory();
		}

		[HttpGet(nameof(Find))]
		[ProducesResponseType(200, Type = typeof(Result<LeagueViewModel>))]
		public ActionResult<Result<LeagueViewModel>> Find([FromQuery] FindLeagueRequest request)
		{
			if (!TryFindLeague(request, out var league))
			{
				return Result<LeagueViewModel>.Failure("Could not find league.");
			}

			var leagueViewModel = _leagueViewModelFactory.CreateDetailedViewModel(league);
			return Result<LeagueViewModel>.Successful(leagueViewModel);
		}

		private bool TryFindLeague(FindLeagueRequest request, out ILeague league)
		{
			if (request.LeagueId.HasValue)
			{
				league = _leagueStore.Find(request.LeagueId.Value);
				return true;
			}

			if (!string.IsNullOrEmpty(request.Path))
			{
				league = _leagueStore.FindByPath(request.Path);
				return true;
			}

			league = null;
			return false;
		}

		private readonly ILeagueStore _leagueStore;
		private readonly ILeagueViewModelFactory _leagueViewModelFactory;
	}

	public class FindLeagueRequest
	{
		public Guid? LeagueId { get; set; }
		public string Path { get; set; }
	}
}
