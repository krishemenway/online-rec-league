using LeagueService.Users.Profiles;
using System;
using System.Collections.Generic;

namespace LeagueService.Users
{
	public interface IUserProfile
	{
		string NickName { get; }
	}

	public interface IUserProfileFactory
	{
		IUserProfile CreateProfile(IUser user, UserProfileType type);
	}

	public class UserProfileFactory : IUserProfileFactory
	{
		public UserProfileFactory()
		{
			_profileCreationFunctions = new Dictionary<UserProfileType, Func<IUser, IUserProfile>>
				{
					{ UserProfileType.PersonalProfile, (user) => new PersonalProfileFactory().Create(user) },
					{ UserProfileType.TeammateProfile, (user) => new TeammateProfileFactory().Create(user) },
				};
		}

		public IUserProfile CreateProfile(IUser user, UserProfileType type)
		{
			return _profileCreationFunctions[type](user);
		}

		private IReadOnlyDictionary<UserProfileType, Func<IUser, IUserProfile>> _profileCreationFunctions;
	}

	public enum UserProfileType
	{
		PersonalProfile,
		TeammateProfile,
		FriendProfile,
		StrangerProfile,
		PrivateProfile
	}
}
