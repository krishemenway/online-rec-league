using System;

namespace LeagueService.Users
{
	public interface IUser
	{
		Guid UserId { get; }

		string NickName { get; }
		string RealName { get; }

		string Email { get; }

		DateTime JoinTime { get; }
		DateTime? QuitTime { get; }
	}

	public class User : IUser
	{
		public User(Guid userId)
		{
			UserId = userId;
		}

		public Guid UserId { get; }

		public string NickName { get; set; }
		public string RealName { get; set; }

		public string Email { get; set; }

		public DateTime JoinTime { get; set; }
		public DateTime? QuitTime { get; set; }
		
		public TimeZoneInfo DefaultTimezone { get; set; }
	}
}
