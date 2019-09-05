using OnlineRecLeague.Service.DataTypes;
using System;

namespace OnlineRecLeague.Users
{
	internal class UserRecord
	{
		public Id<User> UserId { get; set; }

		public string NickName { get; set; }
		public string RealName { get; set; }

		public string Email { get; set; }
		public string PasswordHash { get; set; }

		public Guid EmailConfirmationCode { get; set; }
		public DateTimeOffset? EmailConfirmationTime { get; set; }

		public DateTimeOffset JoinTime { get; set; }
		public DateTimeOffset? QuitTime { get; set; }

		public string Region { get; set; }
		public string DefaultTimezone { get; set; }
	}
}