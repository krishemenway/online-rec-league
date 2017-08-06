using System;

namespace LeagueService.Users
{
	public class CreateNewUserRequest
	{
		public string NickName { get; set; }
		public string RealName { get; set; }
		public string Email { get; set; }
		public DateTime JoinTime { get; set; }
		public string DefaultTimezone { get; set; }
	}
}