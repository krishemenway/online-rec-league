using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.Leagues;
using OnlineRecLeague.Service.DataTypes;
using OnlineRecLeague.Teams;
using OnlineRecLeague.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.LeagueTeams
{
	public interface ILeagueTeamStore
	{
		IReadOnlyList<ILeagueTeam> Find(ILeague league);
	}

	public class LeagueTeamStore : ILeagueTeamStore
	{
		public LeagueTeamStore(ITeamStore teamStore = null)
		{
			_teamStore = teamStore ?? new TeamStore();
		}

		public IReadOnlyList<ILeagueTeam> Find(IReadOnlyList<Id<LeagueTeam>> leagueTeamIds)
		{
			const string sql = @"
				SELECT
					league_team_id as leagueteamid,
					team_id as teamid,
					created_at_time as createdattime,
					created_by_user_id as createdbyuserid
				FROM public.league_team
				WHERE league_id = @LeagueId";

			using (var connection = AppDataConnection.Create())
			{
				var records = connection.Query<LeagueTeamRecord>(sql, new { leagueTeamIds }).ToList();
				var teamByTeamId = _teamStore.FindTeams(records.Select(x => x.TeamId).ToList()).ToDictionary(x => x.TeamId, x => x);

				return records.Select(record => CreateLeagueTeam(record, teamByTeamId[record.TeamId])).ToList();
			}
		}

		public IReadOnlyList<ILeagueTeam> Find(ILeague league)
		{
			const string sql = @"
				SELECT
					league_team_id as leagueteamid,
					team_id as teamid,
					created_at_time as createdattime,
					created_by_user_id as createdbyuserid
				FROM public.league_team
				WHERE league_id = @LeagueId";

			using (var connection = AppDataConnection.Create())
			{
				var records = connection.Query<LeagueTeamRecord>(sql, new { league.LeagueId }).ToList();
				var teamByTeamId = _teamStore.FindTeams(records.Select(x => x.TeamId).ToList()).ToDictionary(x => x.TeamId, x => x);

				return records.Select(record => CreateLeagueTeam(record, teamByTeamId[record.TeamId])).ToList();
			}
		}

		public ILeagueTeam CreateLeagueTeam(LeagueTeamRecord record, ITeam team)
		{
			return new LeagueTeam(record.LeagueTeamId)
				{
					TeamId = team.TeamId,
					Team = team,
					CreatedByUserId = record.CreatedByUserId,
					CreatedAtTime = record.CreatedAtTime
				};
		}

		private ITeamStore _teamStore;
	}

	public class LeagueTeamRecord
	{
		public Id<LeagueTeam> LeagueTeamId { get; set; }
		public Id<Team> TeamId { get; set; }
		public DateTimeOffset CreatedAtTime { get; set; }
		public Id<User> CreatedByUserId { get; set; }
	}
}
