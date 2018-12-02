using System;

namespace OnlineRecLeague.Users
{
	public class CreateNewUserRequest
	{
		public string NickName { get; set; }
		public string RealName { get; set; }
		public string Email { get; set; }
		public DateTimeOffset JoinTime { get; set; }
		public string DefaultTimezone { get; set; }
		public string Region { get; set; }
	}
}