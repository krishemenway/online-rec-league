using Dapper;
using OnlineRecLeague.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.Users
{
	public interface IUserStore
	{
		IUser CreateNewUser(CreateNewUserRequest request);

		bool TryFindUserById(Guid userId, out IUser user);
		bool TryFindUserByEmail(string emailAddress, out IUser user);

		IReadOnlyList<IUser> FindUsers(IReadOnlyList<Guid> userIds);
	}

	internal class UserStore : IUserStore
	{
		public UserStore(IRegionStore regionStore = null)
		{
			_regionStore = regionStore ?? new RegionStore();
		}

		public bool TryFindUserById(Guid userId, out IUser user)
		{
			user = FindUsers(new[] { userId }).FirstOrDefault();
			return user != null;
		}

		public bool TryFindUserByEmail(string email, out IUser user)
		{
			const string sql = @"
				SELECT
					user_id as userid,
					nickname,
					realname,
					email,
					join_time as jointime,
					region,
					default_timezone as defaulttimezone,
					email_validated as emailvalidated
				FROM
					svc.user
				WHERE
					email = @Email";

			using (var connection = Database.CreateConnection())
			{
				user = connection.Query<UserRecord>(sql, new { email }).Select(Create).SingleOrDefault();
				return user != null;
			}
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
					region,
					default_timezone as defaulttimezone,
					email_validated as emailvalidated
				FROM
					svc.user
				WHERE
					user_id = any(@UserIds)";

			using (var connection = Database.CreateConnection())
			{
				return connection.Query<UserRecord>(sql, new { userIds }).Select(Create).ToList();
			}
		}

		public IUser CreateNewUser(CreateNewUserRequest request)
		{
			const string sql = @"
				INSERT INTO svc.user
				(nickname, realname, email, join_time, default_timezone, region, email_validated)
				VALUES
				(@NickName, @RealName, @Email, @JoinTime, @DefaultTimezone, @Region, @EmailValidated)
				RETURNING user_id;";

			using (var connection = Database.CreateConnection())
			{
				var sqlParams = new
					{
						request.NickName,
						request.RealName,
						request.Email,
						request.JoinTime,
						request.DefaultTimezone,
						request.Region,
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

					Region = _regionStore.FindRegionOrThrow(userRecord.Region),
					DefaultTimezone = TimeZoneInfo.FindSystemTimeZoneById(userRecord.DefaultTimezone)
				};
		}

		private readonly IRegionStore _regionStore;
	}
}