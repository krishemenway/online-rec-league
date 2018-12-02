using System;

namespace OnlineRecLeague.Ladders
{
	public class CreateLadderRequest
	{
		public string Name { get; set; }
		public string UriPath { get; set; }
		public Guid SportId { get; set; }
		public string Rules { get; set; }
	}
}
