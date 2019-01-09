using System;

namespace OnlineRecLeague.Users
{
	public class ConfirmEmailRequest
	{
		public Guid? EmailConfirmationCode { get; set; }
	}
}
