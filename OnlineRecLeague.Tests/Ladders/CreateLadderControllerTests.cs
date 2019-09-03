using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;
using System;

namespace OnlineRecLeague.Ladders
{
	[TestFixture]
	public class CreateLadderControllerTests
	{
		[SetUp]
		public void SetUp()
		{
			GivenRequest = new CreateLadderRequest
			{
				Name = "Ladder Name",
				UriPath = "UriPath",
				SportId = Guid.NewGuid(),
			};

			GivenUser = new UserBuilder();

			var controllerContext = new MockControllerContext();

			_leagueStore = new Mock<ILadderStore>();
			_leagueViewModelFactory = new Mock<ILadderViewModelFactory>();

			_userSessionStore = new Mock<IUserSessionStore>();
			_userSessionStore.Setup(x => x.FindUserOrThrow(controllerContext.HttpContext.Session)).Returns(GivenUser.Instance);

			_createLadderController = new CreateLadderController(_leagueStore.Object, _leagueViewModelFactory.Object, _userSessionStore.Object)
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
			ThenResult = _createLadderController.Create(GivenRequest).Value;
		}

		private CreateLadderRequest GivenRequest { get; set; }
		private UserBuilder GivenUser { get; set; }
		private Result<LadderViewModel> ThenResult { get; set; }

		private Mock<ILadderStore> _leagueStore;
		private Mock<IUserSessionStore> _userSessionStore;
		private Mock<ILadderViewModelFactory> _leagueViewModelFactory;

		private CreateLadderController _createLadderController;
	}
}
