using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;
using System;

namespace OnlineRecLeague.Leagues
{
	[TestFixture]
	public class CreateLeagueControllerTests
	{
		[SetUp]
		public void SetUp()
		{
			GivenRequest = new CreateLeagueRequest
			{
				Name = "League Name",
				UriPath = "UriPath",
				SportId = Guid.NewGuid(),
			};

			GivenUser = new UserBuilder();

			var controllerContext = new MockControllerContext();

			_leagueStore = new Mock<ILeagueStore>();
			_leagueViewModelFactory = new Mock<ILeagueViewModelFactory>();

			_userSessionStore = new Mock<IUserSessionStore>();
			_userSessionStore.Setup(x => x.FindUserOrThrow(controllerContext.HttpContext.Session)).Returns(GivenUser.Instance);

			_createLeagueController = new CreateLeagueController(_leagueStore.Object, _leagueViewModelFactory.Object, _userSessionStore.Object)
			{
				ControllerContext = controllerContext
			};
		}

		[Test]
		public void Test()
		{
			WhenHandlingRequest();
			ThenResult.Success.Should().BeTrue();
		}

		private void WhenHandlingRequest()
		{
			ThenResult = _createLeagueController.Create(GivenRequest).Value;
		}

		private CreateLeagueRequest GivenRequest { get; set; }
		private UserBuilder GivenUser { get; set; }
		private Result<LeagueViewModel> ThenResult { get; set; }

		private Mock<ILeagueStore> _leagueStore;
		private Mock<IUserSessionStore> _userSessionStore;
		private Mock<ILeagueViewModelFactory> _leagueViewModelFactory;

		private CreateLeagueController _createLeagueController;
	}
}
