using OnlineRecLeague.Service.DataTypes;

namespace OnlineRecLeague.Users
{
	public class FindProfileRequest
	{
		public Id<User> UserId { get; set; }
	}
}
