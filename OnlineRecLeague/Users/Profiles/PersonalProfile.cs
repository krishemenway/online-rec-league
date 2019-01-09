using OnlineRecLeague.Regions;
using System;

namespace OnlineRecLeague.Users.Profiles
{
	public class PersonalProfile : IUserProfile
	{
		public PersonalProfile(IUser user)
		{
			_user = user;
		}

		public UserProfileType ProfileType => UserProfileType.PersonalProfile;

		public string NickName => _user.NickName;

		public string Email => _user.Email;
		public bool EmailIsConfirmed => _user.EmailConfirmedTime.HasValue;

		public DateTimeOffset JoinTime => _user.JoinTime;

		public TimeZoneInfo DefaultTimezone => _user.DefaultTimezone;
		public IRegion Region => _user.Region;

		public bool IsSuperAdmin => _user.IsSuperAdmin;

		private readonly IUser _user;
	}

	public class PersonalProfileFactory
	{
		public IUserProfile Create(IUser user)
		{
			return new PersonalProfile(user);
		}
	}
}
