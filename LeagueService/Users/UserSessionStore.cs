using Microsoft.AspNetCore.Http;
using System;

namespace LeagueService.Users
{
	public interface IUserSessionStore
	{
		IUser FindUser(ISession session);
		void SetLoggedInUser(ISession session, IUser user);
	}

	internal class UserSessionStore : IUserSessionStore
	{
		public UserSessionStore(IUserStore userStore = null)
		{
			_userStore = userStore ?? new UserStore();
		}

		public IUser FindUser(ISession session)
		{
			if (!Guid.TryParse(session.GetString("LoggedInUser"), out var userId))
			{
				return null;
			}

			if (_threadCachedUser?.UserId == userId)
			{
				return _threadCachedUser;
			}

			if(!_userStore.TryFindUserById(userId, out _threadCachedUser))
			{
				return null;
			}

			return _threadCachedUser;
		}

		public void SetLoggedInUser(ISession session, IUser user)
		{
			session.SetString("LoggedInUser", user.UserId.ToString());
		}

		[ThreadStatic]
		private static IUser _threadCachedUser;

		private readonly IUserStore _userStore;
	}
}
