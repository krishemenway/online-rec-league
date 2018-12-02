namespace OnlineRecLeague.Users.Profiles
{
	public class TeammateProfile : IUserProfile
	{
		public TeammateProfile(IUser user)
		{
			_user = user;
		}

		public string NickName => _user.NickName;
		public string Email => _user.Email;

		private readonly IUser _user;
	}

	public class TeammateProfileFactory
	{
		public IUserProfile Create(IUser user)
		{
			return new TeammateProfile(user);
		}
	}
}
