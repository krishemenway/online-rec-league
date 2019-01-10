using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineRecLeague.DataTypes;

namespace OnlineRecLeague.Users
{
	public class RequiresAdminAccessAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!new UserSessionStore().TryFindUser(context.HttpContext.Session, out var user) || !user.IsAdmin)
			{
				context.Result = new JsonResult(Result.Failure("You must be logged in as superadmin to perform this action"));
			}
		}
	}
}