using System;
using System.Collections.Generic;

namespace OnlineRecLeague.Users.Profiles
{
	public interface IUserProfileFactory
	{
		IUserProfile CreateProfile(IUser user, UserProfileType type);
	}

	internal class UserProfileFactory : IUserProfileFactory
	{
		public UserProfileFactory()
		{
			_profileCreationFunctions = new Dictionary<UserProfileType, Func<IUser, IUserProfile>>
				{
					{ UserProfileType.StrangerProfile, (user) => new StrangerProfileFactory().Create(user) },
					{ UserProfileType.PersonalProfile, (user) => new PersonalProfileFactory().Create(user) },
					{ UserProfileType.TeammateProfile, (user) => new TeammateProfileFactory().Create(user) },
					{ UserProfileType.FriendProfile, (user) => throw new NotImplementedException() },
					{ UserProfileType.PrivateProfile, (user) => throw new NotImplementedException() },
				};
		}

		public IUserProfile CreateProfile(IUser user, UserProfileType type)
		{
			return _profileCreationFunctions[type](user);
		}

		private IReadOnlyDictionary<UserProfileType, Func<IUser, IUserProfile>> _profileCreationFunctions;
	}
}
