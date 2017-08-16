using System;

namespace LeagueService.CoreExtensions
{
	public static class TimeZoneInfoExtensions
	{
		public static DateTime CurrentTime(this TimeZoneInfo timeZoneInfo)
		{
			return TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZoneInfo);
		}
	}
}
