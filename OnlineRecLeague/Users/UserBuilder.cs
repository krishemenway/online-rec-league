using Moq;

namespace OnlineRecLeague.Users
{
	public class UserBuilder
	{
		public UserBuilder()
		{
			MockInstance = new Mock<IUser>();
		}

		internal IUser Instance => MockInstance.Object;

		private Mock<IUser> MockInstance;
	}
}
