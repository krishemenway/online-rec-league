using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineRecLeague.CommonDataTypes;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OnlineRecLeague
{
	public class ExceptionHandler
	{
		public ExceptionHandler(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			Log.Error(exception, "RequestException");
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			return context.Response.WriteAsync(JsonConvert.SerializeObject(Result.Failure("Something went wrong with this request")));
		}

		private readonly RequestDelegate next;
	}
}
