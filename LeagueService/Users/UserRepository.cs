using LeagueService.CommonDataTypes;
using Microsoft.AspNetCore.Http;
using System;

namespace LeagueService.Users
{
	public interface IUserRepository
	{
		Result<IUserProfile> FindProfile(Guid userId, IUser loggedInUser);
		Result<IUserProfile> CreateUser(CreateNewUserRequest request, ISession session);
	}

	internal class UserRepository : IUserRepository
	{
		public UserRepository(
			IUserStore userStore = null,
			IUserSessionStore userSessionStore = null,
			IUserProfileFactory userProfileFactory = null)
		{
			_userStore = userStore ?? new UserStore();
			_userSessionStore = userSessionStore ?? new UserSessionStore();
			_userProfileFactory = userProfileFactory ?? new UserProfileFactory();
		}

		public Result<IUserProfile> FindProfile(Guid userId, IUser loggedInUser)
		{
			if(!_userStore.TryFindUserById(userId, out var user))
			{
				return Result<IUserProfile>.Failure("User does not exist");
			}

			var profileType = user.UserId == loggedInUser.UserId ? UserProfileType.PersonalProfile : UserProfileType.StrangerProfile;
			return Result<IUserProfile>.Successful(_userProfileFactory.CreateProfile(user, profileType));
		}

		public Result<IUserProfile> CreateUser(CreateNewUserRequest request, ISession session)
		{
			request.JoinTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(request.DefaultTimezone));

			var newUser = _userStore.CreateNewUser(request);
			_userSessionStore.SetLoggedInUser(session, newUser);

			return Result<IUserProfile>.Successful(_userProfileFactory.CreateProfile(newUser, UserProfileType.PersonalProfile));
		}

		private readonly IUserStore _userStore;
		private readonly IUserSessionStore _userSessionStore;
		private readonly IUserProfileFactory _userProfileFactory;
	}
}
