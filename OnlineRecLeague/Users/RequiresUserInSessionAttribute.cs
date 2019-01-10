using OnlineRecLeague.DataTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineRecLeague.Users
{
	public class RequiresUserInSessionAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!new UserSessionStore().TryFindUser(context.HttpContext.Session, out var _))
			{
				context.Result = new JsonResult(Result.Failure("You must be logged in to perform this action"));
			}
		}
	}
}