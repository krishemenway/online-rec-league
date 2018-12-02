using System;

namespace OnlineRecLeague.Teams
{
	public class TeamRecord
	{
		public Guid TeamId { get; set; }
		public string Name { get; set; }
		public string ProfileContent { get; set; }
		public string UserNamePrefix { get; set; }
		public Guid OwnerUserId { get; set; }
		public DateTime CreatedTime { get; set; }
	}
}
