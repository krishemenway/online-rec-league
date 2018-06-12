using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRecLeague.Users.Profiles
{
	public class TeammateUserProfile : IUserProfile
	{
		public string NickName => throw new NotImplementedException();
	}

	public class TeammateProfileFactory
	{
		public IUserProfile Create(IUser user)
		{
			return new TeammateUserProfile();
		}
	}
}
