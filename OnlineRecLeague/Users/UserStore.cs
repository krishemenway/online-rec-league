using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Users
{
	internal interface IUserStore
	{
		bool TryFindUserById(Guid userId, out IUser user);
		IReadOnlyList<IUser> FindUsers(IReadOnlyList<Guid> userIds);

		IUser CreateNewUser(CreateNewUserRequest request);
	}

	internal class UserStore : IUserStore
	{
		public bool TryFindUserById(Guid userId, out IUser user)
		{
			user = FindUsers(new[] { userId }).FirstOrDefault();
			return user != null;
		}

		public IReadOnlyList<IUser> FindUsers(IReadOnlyList<Guid> userIds)
		{
			const string sql = @"
				SELECT
					user_id as userid,
					nickname,
					realname,
					email,
					join_time as jointime,
					default_timezone as defaulttimezone,
					email_validated as emailvalidated
				FROM
					svc.user
				WHERE
					user_id = any(@UserIds)";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<UserRecord>(sql, new { userIds }).Select(Create).ToList();
			}
		}

		public IUser CreateNewUser(CreateNewUserRequest request)
		{
			const string sql = @"
				INSERT INTO svc.user
				(nickname, realname, email, join_time, default_timezone, email_validated)
				VALUES
				(@NickName, @RealName, @Email, @JoinTime, @DefaultTimezone, @EmailValidated)
				RETURNING user_id;";

			using (var connection = AppDataConnection.Create())
			{
				var sqlParams = new
					{
						request.NickName,
						request.RealName,
						request.Email,
						request.JoinTime,
						request.DefaultTimezone,
						EmailValidated = false
					};

				var userId = connection.QuerySingle<Guid>(sql, sqlParams);
				return FindUsers(new[] { userId }).Single();
			}
		}

		public IUser Create(UserRecord userRecord)
		{
			return new User(userRecord.UserId)
				{
					NickName = userRecord.NickName,
					RealName = userRecord.RealName,

					Email = userRecord.Email,

					JoinTime = userRecord.JoinTime,
					QuitTime = userRecord.QuitTime,

					DefaultTimezone = TimeZoneInfo.FindSystemTimeZoneById(userRecord.DefaultTimezone)
				};
		}
	}

	internal class UserRecord
	{
		public Guid UserId { get; set; }

		public string NickName { get; set; }
		public string RealName { get; set; }

		public string Email { get; set; }

		public DateTime JoinTime { get; set; }
		public DateTime? QuitTime { get; set; }

		public string DefaultTimezone { get; set; }
	}
}