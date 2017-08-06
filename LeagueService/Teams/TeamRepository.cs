using LeagueService.CommonDataTypes;
using LeagueService.Users;
using System;

namespace LeagueService.Teams
{
	public interface ITeamRepository
	{
		Result<ITeamProfile> CreateTeam(CreateTeamRequest createTeamRequest, IUser teamCreator);
	}

	public class TeamRepository : ITeamRepository
	{
		public Result<ITeamProfile> CreateTeam(CreateTeamRequest createTeamRequest, IUser teamCreator)
		{
			throw new NotImplementedException();
		}
	}
}
