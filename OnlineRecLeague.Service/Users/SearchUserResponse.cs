using OnlineRecLeague.Users.Profiles;
using System.Collections.Generic;

namespace OnlineRecLeague.Users
{
	public class SearchUserResponse
	{
		public IReadOnlyList<IUserProfile> Users { get; set; }
	}
}
