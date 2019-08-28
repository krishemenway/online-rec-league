using System;

namespace OnlineRecLeague.Leagues
{
	public class CreateLeagueRequest
	{
		public string Name { get; set; }
		public string UriPath { get; set; }

		public Guid GameId { get; set; }
		public Guid CreatedByUserId { get; set; }
	}
}
