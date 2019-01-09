using Dapper;
using Microsoft.Extensions.HealthChecks;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineRecLeague.AppData
{
	public class AppDataHealthCheck : IHealthCheck
	{
		public ValueTask<IHealthCheckResult> CheckAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				using (var connection = AppDataConnection.Create())
				{
					connection.Execute(string.Join(" ", AppDataSchema.Tables.Select(x => $"SELECT 1 from {x.Key};")));
				}

				return new ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Everything is good, nothing to see here"));
			}
			catch (Exception exception)
			{
				return new ValueTask<IHealthCheckResult>(HealthCheckResult.Unhealthy($"AppData failed: {exception.Message}"));
			}
		}
	}
}
