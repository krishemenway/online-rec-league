using System;

namespace OnlineRecLeague.Users
{
	internal class UserRecord
	{
		public Guid UserId { get; set; }

		public string NickName { get; set; }
		public string RealName { get; set; }

		public string Email { get; set; }

		public DateTimeOffset JoinTime { get; set; }
		public DateTimeOffset? QuitTime { get; set; }

		public string Region { get; set; }
		public string DefaultTimezone { get; set; }
	}
}