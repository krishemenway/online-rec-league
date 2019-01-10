using Microsoft.Extensions.Logging;

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
			new LoggerFactory().CreateLogger<EmailSender>().LogInformation($@"Email Sent to {emailAddress} \nSubject: {subject} \nBody: {body}");
			// TODO: implement real email sending
		}
	}
}
