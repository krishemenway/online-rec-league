using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.Leagues;
using OnlineRecLeague.LeagueTeams;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.LeagueMatches
{
	public interface ILeagueMatchStore
	{
		IReadOnlyList<ILeagueMatch> FindAll(ILeague league);
		IReadOnlyList<ILeagueMatch> FindAll(ILeagueTeam leagueTeam);

		void Create(CreateMatchRequest saveLeagueMatchRequest);
		void SetMatchTime(Guid leagueMatchId, DateTimeOffset matchTime);
		void ReportResults(ILeagueMatch match, bool matchSuccessful, string matchResults, DateTimeOffset matchResultsReportedTime);
	}

	internal class LeagueMatchStore : ILeagueMatchStore
	{
		public IReadOnlyList<ILeagueMatch> FindAll(ILeague league)
		{
			const string sql = @"
				SELECT
					league_match_id as leaguematchid,
					league_id as leagueid,
					home_match_team_id as homematchleagueteamid,
					away_match_team_id as awaymatchleagueteamid,
					match_created_time as matchcreatedtime,
					match_start_time as matchstarttime,
					match_finish_time as matchfinishtime,
					match_results as matchesults,
					match_results_reported_time as matchesultsreportedtime
				FROM public.league_match
				WHERE league_id = @LeagueId";

			using (var connection = AppDataConnection.Create())
			{
				var records = connection.Query<LeagueMatchRecord>(sql, new { league.LeagueId }).ToList();
				var leagueTeamIds = records.SelectMany(x => new[] { x.AwayLeagueTeamId, x.HomeLeagueTeamId}).ToList();
				var leagueTeamsByLeagueTeamId = new LeagueTeamStore().Find(leagueTeamIds).ToDictionary(x => x.LeagueTeamId, x => x);

				return records.Select(record => Create(record, leagueTeamsByLeagueTeamId)).ToList();
			}
		}

		public IReadOnlyList<ILeagueMatch> FindAll(ILeagueTeam leagueTeam)
		{
			const string sql = @"
				SELECT
					league_match_id as leaguematchid,
					league_id as leagueid,
					home_match_team_id as homematchleagueteamid,
					away_match_team_id as awaymatchleagueteamid,
					match_created_time as matchcreatedtime,
					match_start_time as matchstarttime,
					match_finish_time as matchfinishtime,
					match_results as matchesults,
					match_results_reported_time as matchesultsreportedtime
				FROM public.league_match
				WHERE league_team_id = @LeagueTeamId";

			using (var connection = AppDataConnection.Create())
			{
				var records = connection.Query<LeagueMatchRecord>(sql, new { leagueTeam.LeagueTeamId }).ToList();
				var leagueTeamIds = records.SelectMany(x => new[] { x.AwayLeagueTeamId, x.HomeLeagueTeamId }).ToList();
				var leagueTeamsByLeagueTeamId = new LeagueTeamStore().Find(leagueTeamIds).ToDictionary(x => x.LeagueTeamId, x => x);

				return records.Select(record => Create(record, leagueTeamsByLeagueTeamId)).ToList();
			}
		}

		public void Create(CreateMatchRequest createMatchRequest)
		{
			const string sql = @"
				INSERT INTO public.league_match
				(league_id, match_league_team_id, match_league_team_id, match_created_time)
				VALUES 
				(@LeagueId, @ChallengerLeagueTeamId, @ChallengedLeagueTeamId, @MatchCreatedTime)";

			using (var connection = AppDataConnection.Create())
			{
				var sqlParams = new
				{
					createMatchRequest.LeagueId,
					createMatchRequest.AwayLeagueTeamId,
					createMatchRequest.HomeLeagueTeamId,
					MatchCreatedTime = DateTimeOffset.Now
				};

				connection.Execute(sql, createMatchRequest);
			}
		}

		public void SetMatchTime(Guid leagueMatchId, DateTimeOffset matchTime)
		{
			const string sql = @"
				UPDATE public.league_match
				SET match_start_time = @MatchTime
				WHERE league_match_id = @LeagueMatchId";

			using (var connection = AppDataConnection.Create())
			{
				connection.Execute(sql, new { leagueMatchId, matchTime });
			}
		}

		public void ReportResults(ILeagueMatch match, bool winningLeagueTeamId, string matchResults, DateTimeOffset matchResultsReportedTime)
		{
			const string sql = @"
				UPDATE public.league_match
				SET
					winning_league_team_id = @WinningLeagueTeamId,
					match_results = @MatchResults,
					match_results_reported_time = @MatchResultsReportedTime
				WHERE
					league_match_id = @LeagueMatchId";

			using (var connection = AppDataConnection.Create())
			{
				connection.Execute(sql, new { winningLeagueTeamId, matchResults, matchResultsReportedTime, match.LeagueMatchId });
			}
		}

		private ILeagueMatch Create(LeagueMatchRecord record, IReadOnlyDictionary<Guid, ILeagueTeam> leagueTeamsByLeagueTeamId)
		{
			return new LeagueMatch
			{
				LeagueMatchId = record.LeagueMatchId,
				LeagueId = record.LeagueId,
					
				HomeLeagueTeamId = record.HomeLeagueTeamId,
				AwayLeagueTeamId = record.AwayLeagueTeamId,
			};
		}
	}

	public class LeagueMatchRecord
	{
		public Guid LeagueMatchId { get; set; }
		public Guid LeagueId { get; set; }

		public Guid HomeLeagueTeamId { get; set; }
		public Guid AwayLeagueTeamId { get; set; }

		public DateTimeOffset MatchCreatedTime { get; set; }
		public DateTimeOffset? MatchStartTime { get; set; }
		public DateTimeOffset? MatchResultsReportedTime { get; set; }

		public string MatchResultsJson { get; set; }

		public Guid WinningLeagueTeamId { get; set; }
	}
}
