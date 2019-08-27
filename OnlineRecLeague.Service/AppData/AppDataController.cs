using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace OnlineRecLeague.AppData
{
	[Route("api")]
	public class AppDataController : Controller
	{
		[HttpGet("schema")]
		public IActionResult Schema()
		{
			return File(Encoding.UTF8.GetBytes(AppDataSchema.CreateSchemaScript()), "application/sql", "schema.sql");
		} 
	}
}
