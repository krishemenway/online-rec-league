using OnlineRecLeague.DataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Teams
{
	public interface ICreateTeamRequestHandler
	{
		Result<ITeamProfile> CreateTeam(CreateTeamRequest createTeamRequest, IUser teamCreator);
	}

	internal class CreateTeamRequestHandler : ICreateTeamRequestHandler
	{
		public CreateTeamRequestHandler(
			ITeamStore teamStore = null,
			ITeamProfileFactory teamProfileFactory = null)
		{
			_teamStore = teamStore ?? new TeamStore();
			_teamProfileFactory = teamProfileFactory ?? new TeamProfileFactory();
		}

		public Result<ITeamProfile> CreateTeam(CreateTeamRequest request, IUser teamCreator)
		{
			var team = _teamStore.CreateTeam(request, teamCreator);
			var teamProfile = _teamProfileFactory.Create(team);

			return Result<ITeamProfile>.Successful(teamProfile);
		}

		private readonly ITeamStore _teamStore;
		private readonly ITeamProfileFactory _teamProfileFactory;
	}
}
