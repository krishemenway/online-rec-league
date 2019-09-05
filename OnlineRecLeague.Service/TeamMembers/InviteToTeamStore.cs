using Dapper;
using OnlineRecLeague.AppData;
using OnlineRecLeague.Service.DataTypes;
using OnlineRecLeague.Teams;
using OnlineRecLeague.Users;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRecLeague.TeamMembers
{
	public interface IInviteToTeamStore
	{
		IReadOnlyList<Id<Team>> FindOpenInvitesForUsers(IUser user);
		void SaveInviteToTeam(ITeam team, string email);
		void RemoveInviteToTeam(ITeam team, string email);
	}

	public class InviteToTeamStore : IInviteToTeamStore
	{
		public IReadOnlyList<Id<Team>> FindOpenInvitesForUsers(IUser user)
		{
			const string sql = @"
				SELECT team_id
				FROM public.invite_to_team
				WHERE email = @Email";

			using (var connection = AppDataConnection.Create())
			{
				return connection.Query<Id<Team>>(sql, new { user.Email }).ToList();
			}
		}

		public void SaveInviteToTeam(ITeam team, string email)
		{
			const string sql = @"
				INSERT INTO public.invite_to_team
				(team_id, email)
				VALUES
				(@TeamId, @Email)";

			using (var connection = AppDataConnection.Create())
			{
				connection.Execute(sql, new { team.TeamId, email });
			}
		}

		public void RemoveInviteToTeam(ITeam team, string email)
		{
			const string sql = @"
				DELETE FROM public.invite_to_team
				WHERE team_id = @TeamId AND Email = @Email";

			using (var connection = AppDataConnection.Create())
			{
				connection.Execute(sql, new { team.TeamId, email });
			}
		}
	}
}
