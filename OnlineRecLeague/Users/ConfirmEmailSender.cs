using System;

namespace OnlineRecLeague.Users
{
	public interface IConfirmEmailSender
	{
		void SendConfirmEmail(IUser user);
	}

	public class ConfirmEmailSender : IConfirmEmailSender
	{
		public void SendConfirmEmail(IUser user)
		{
			throw new NotImplementedException();
		}
	}
}
