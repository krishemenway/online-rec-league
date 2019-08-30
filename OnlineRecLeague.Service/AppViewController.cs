using Microsoft.AspNetCore.Mvc;

namespace OnlineRecLeague.Service.Leagues
{
	[Route("")]
	[Route("league")]
	[Route("ladder")]
	[Route("game")]
	[Route("sport")]
	public class AppViewController : ControllerBase
	{
		[HttpGet("{*path}")]
		public ActionResult AppView(string path)
		{
			Response.ContentType = "text/html; charset=UTF-8";
			return Content(View);
		}

		private const string View = @"
				<html>
					<head></head>
					<body>
						<div id=""app"" />
						<script src=""/assets/app.js""></script>
						<script type=""text/javascript"">
							window.initializeApp();
						</script>
					</body>
				</html>";
	}
}
