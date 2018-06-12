using Microsoft.AspNetCore.Http;
using System;

namespace OnlineRecLeague.Users
{
	public interface IUserSessionStore
	{
		IUser FindUserOrThrow(ISession session);
		bool TryFindUser(ISession session, out IUser user);
		void SetLoggedInUser(ISession session, IUser user);
	}

	internal class UserSessionStore : IUserSessionStore
	{
		public UserSessionStore(IUserStore userStore = null)
		{
			_userStore = userStore ?? new UserStore();
		}

		public bool TryFindUser(ISession session, out IUser user)
		{
			user = null;

			if (!Guid.TryParse(session.GetString("LoggedInUser"), out var userId))
			{
				return false;
			}

			if(_threadCachedUser?.UserId != userId || !_userStore.TryFindUserById(userId, out _threadCachedUser))
			{
				return false;
			}

			user = _threadCachedUser;
			return true;
		}

		public void SetLoggedInUser(ISession session, IUser user)
		{
			session.SetString("LoggedInUser", user.UserId.ToString());
		}

		public IUser FindUserOrThrow(ISession session)
		{
			if(!TryFindUser(session, out var user))
			{
				throw new InvalidUserSessionException();
			}

			return user;
		}

		[ThreadStatic]
		private static IUser _threadCachedUser;

		private readonly IUserStore _userStore;
	}

	public class InvalidUserSessionException : Exception
	{
		public InvalidUserSessionException() : base("Requested user from session but user session was unavailable. Use RequiresLoggedInUserAttribute on controller") { }
	}
}
