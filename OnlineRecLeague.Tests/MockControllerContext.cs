using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace OnlineRecLeague
{
	public class MockControllerContext : ControllerContext 
	{
		public MockControllerContext()
		{
			MockSession = new Mock<ISession>();

			MockHttpContext = new Mock<HttpContext>();
			MockHttpContext.Setup(x => x.Session).Returns(MockSession.Object);

			HttpContext = MockHttpContext.Object;
		}

		private Mock<ISession> MockSession { get; set; }
		private Mock<HttpContext> MockHttpContext { get; set; }
	}
}
