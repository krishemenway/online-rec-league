namespace OnlineRecLeague.Users.Profiles
{
	public class StrangerProfile : IUserProfile
	{
		public StrangerProfile(IUser user)
		{
			_user = user;
		}

		public UserProfileType ProfileType => UserProfileType.StrangerProfile;

		public string NickName => _user.NickName;

		private readonly IUser _user;
	}

	public class StrangerProfileFactory
	{
		public StrangerProfile Create(IUser user)
		{
			return new StrangerProfile(user);
		}
	}
}
