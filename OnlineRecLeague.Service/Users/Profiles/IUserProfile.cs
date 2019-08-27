namespace OnlineRecLeague.Users.Profiles
{
	public interface IUserProfile
	{
		UserProfileType ProfileType { get; }
		string NickName { get; }
	}
}
