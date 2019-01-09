using System;

namespace OnlineRecLeague.Leagues
{
	public class CreateLeagueRequest
	{
		string Name { get; }
		string UriPath { get; }

		Guid GameId { get; }
		Guid CreatedByUserId { get; }
	}
}
