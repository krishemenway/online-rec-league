using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineRecLeague.AppData
{
	public class AppDataHealthCheck : IHealthCheck
	{
		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				using (var connection = AppDataConnection.Create())
				{
					connection.Execute(string.Join(" ", AppDataSchema.Tables.Select(x => $"SELECT 1 from {x.Key};")));
				}

				return Task.FromResult(HealthCheckResult.Healthy("Everything is good, how are you?"));
			}
			catch (Exception exception)
			{
				return Task.FromResult(HealthCheckResult.Unhealthy($"AppData failed: {exception.Message}"));
			}
		}
	}
}
