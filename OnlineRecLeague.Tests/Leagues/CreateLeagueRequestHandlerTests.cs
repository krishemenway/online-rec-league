using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Leagues
{
	[TestFixture]
	public class CreateLeagueRequestHandlerTests
	{
		[SetUp]
		public void SetUp()
		{
			GivenRequest = new CreateLeagueRequest();
			GivenUser = new UserBuilder();

			_leagueStore = new Mock<ILeagueStore>();
			_createLeagueRequestHandler = new CreateLeagueRequestHandler(_leagueStore.Object);
		}
		
		[Test]
		public void Test()
		{
			WhenHandlingRequest();
			ThenResult.Success.Should().BeTrue();
		}

		private void WhenHandlingRequest()
		{
			ThenResult = _createLeagueRequestHandler.HandleRequest(GivenRequest, GivenUser.Instance);
		}

		private CreateLeagueRequest GivenRequest { get; set; }
		private UserBuilder GivenUser { get; set; }
		private Result<LeagueViewModel> ThenResult { get; set; }

		private Mock<ILeagueStore> _leagueStore;
		private CreateLeagueRequestHandler _createLeagueRequestHandler;
	}
}
