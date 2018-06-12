using OnlineRecLeague.CommonDataTypes;
using OnlineRecLeague.Users;
using System;

namespace OnlineRecLeague.Teams
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
