using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace OnlineRecLeague.Users.Profiles
{
	public interface IUserProfileFactory
	{
		IUserProfile CreateProfile(IUser user, ISession session);
	}

	internal class UserProfileFactory : IUserProfileFactory
	{
		public UserProfileFactory(IUserSessionStore userSessionStore = null)
		{
			_userSessionStore = userSessionStore ?? new UserSessionStore();

			_profileCreationFunctions = new Dictionary<UserProfileType, Func<IUser, IUserProfile>>
				{
					{ UserProfileType.StrangerProfile, (user) => new StrangerProfileFactory().Create(user) },
					{ UserProfileType.PersonalProfile, (user) => new PersonalProfileFactory().Create(user) },
					{ UserProfileType.TeammateProfile, (user) => new TeammateProfileFactory().Create(user) },
					{ UserProfileType.FriendProfile, (user) => throw new NotImplementedException() },
					{ UserProfileType.PrivateProfile, (user) => throw new NotImplementedException() },
				};
		}

		public IUserProfile CreateProfile(IUser user, ISession session)
		{
			var profileType = DetermineProfileType(user, session);
			return _profileCreationFunctions[profileType](user);
		}

		private UserProfileType DetermineProfileType(IUser user, ISession session)
		{
			if (!_userSessionStore.TryFindUser(session, out var userFromSession))
			{
				return UserProfileType.StrangerProfile;
			}

			if (userFromSession == null)
			{
				return UserProfileType.StrangerProfile;
			}

			if (user == userFromSession)
			{
				return UserProfileType.PersonalProfile;
			}

			return UserProfileType.StrangerProfile;
		}

		private IReadOnlyDictionary<UserProfileType, Func<IUser, IUserProfile>> _profileCreationFunctions;
		private readonly IUserSessionStore _userSessionStore;
	}
}
