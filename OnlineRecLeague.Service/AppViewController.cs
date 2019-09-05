using Microsoft.AspNetCore.Mvc;

namespace OnlineRecLeague.Service.Leagues
{
	public class RootViewController : ControllerBase
	{
		[HttpGet("")]
		public ActionResult RootView()
		{
			Response.ContentType = "text/html; charset=UTF-8";
			return Content(AppViewController.View);
		}
	}

	[Route("l")]
	[Route("g")]
	public class AppViewController : ControllerBase
	{
		[HttpGet("{*path}")]
		public ActionResult AppView()
		{
			Response.ContentType = "text/html; charset=UTF-8";
			return Content(View);
		}

		internal const string View = @"
				<html>
					<head></head>
					<body>
						<div id=""app"" />
						<script type=""text/javascript"" src=""///cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js""></script>
						<script type=""text/javascript"" src=""///cdnjs.cloudflare.com/ajax/libs/knockout/3.5.0/knockout-min.js""></script>
						<script type=""text/javascript"" src=""///unpkg.com/jss@9.8.7/dist/jss.min.js""></script>
						<script type=""text/javascript"" src=""///unpkg.com/jss-preset-default@4.5.0/dist/jss-preset-default.min.js""></script>
						<script type=""text/javascript"">jss.default.setup(jssPreset.default())</script>

						<script src=""/assets/app.js""></script>
						<script type=""text/javascript"">
							window.initializeApp(document.getElementById(""app""));
						</script>
					</body>
				</html>";
	}
}
