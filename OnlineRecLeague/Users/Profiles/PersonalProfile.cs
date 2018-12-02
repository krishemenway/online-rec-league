namespace OnlineRecLeague.Users.Profiles
{
	public class PersonalProfile : IUserProfile
	{
		public PersonalProfile(IUser user)
		{
			_user = user;
		}

		public string NickName => _user.NickName;
		public string Email => _user.Email;

		private readonly IUser _user;
	}

	public class PersonalProfileFactory
	{
		public IUserProfile Create(IUser user)
		{
			return new PersonalProfile(user);
		}
	}
}
