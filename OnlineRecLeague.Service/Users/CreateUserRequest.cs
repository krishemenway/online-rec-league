using System;

namespace OnlineRecLeague.Users
{
	public class CreateUserRequest
	{
		public string Email { get; set; }
		public string Password { get; set; }

		public string NickName { get; set; }
		public string RealName { get; set; }

		public DateTimeOffset JoinTime { get; set; }
		public string DefaultTimezone { get; set; }
	}
}