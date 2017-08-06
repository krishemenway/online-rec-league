namespace LeagueService.Users.Profiles
{
	public class PersonalProfile : IUserProfile
	{
		public string NickName { get; set; }
	}

	public class PersonalProfileFactory
	{
		public IUserProfile Create(IUser user)
		{
			return new PersonalProfile()
				{
					NickName = user.NickName
				};
		}
	}
}
