using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace OnlineRecLeague.Email
{
	public interface IEmailSender
	{
		void SendEmail(string emailAddress, string subject, string body);
	}

	public class EmailSender : IEmailSender
	{
		public void SendEmail(string emailAddress, string subject, string body)
		{
			if (Program.Settings.GetValue<bool>("SupportsSendingEmails"))
			{
				throw new NotImplementedException("Gotta get some smtp information from the config and then find a library or something to send some emails, ya know?");
			}
			else
			{
				new LoggerFactory().CreateLogger<EmailSender>().LogInformation($@"Email Sent to {emailAddress} \nSubject: {subject} \nBody: {body}");
			}
		}
	}
}
