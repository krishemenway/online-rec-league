#if DEBUG
using FluentAssertions;
using Moq;
using OnlineRecLeague.CommonDataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Leagues
{
	public class CreateLeagueRequestHandlerTests
	{
		public void SetUp()
		{
			GivenRequest = new CreateLeagueRequest();
			GivenUser = new UserBuilder();

			_leagueStore = new Mock<ILeagueStore>();
			_createLeagueRequestHandler = new CreateLeagueRequestHandler(_leagueStore.Object);
		}
		
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
		private Result ThenResult { get; set; }

		private Mock<ILeagueStore> _leagueStore;
		private CreateLeagueRequestHandler _createLeagueRequestHandler;
	}
}
#endif
