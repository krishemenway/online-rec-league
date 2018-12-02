using OnlineRecLeague.Regions;
using System;

namespace OnlineRecLeague.Users
{
	public interface IUser
	{
		Guid UserId { get; }

		string NickName { get; }
		string RealName { get; }

		string Email { get; }

		DateTimeOffset JoinTime { get; }
		DateTimeOffset? QuitTime { get; }

		IRegion Region { get; }
		TimeZoneInfo DefaultTimezone { get; }
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

		public DateTimeOffset JoinTime { get; set; }
		public DateTimeOffset? QuitTime { get; set; }

		public IRegion Region { get; set; }
		public TimeZoneInfo DefaultTimezone { get; set; }

		public override bool Equals(object other)
		{
			return other is IUser otherUser && UserId.Equals(otherUser.UserId);
		}

		public override int GetHashCode()
		{
			return UserId.GetHashCode();
		}
	}
}
