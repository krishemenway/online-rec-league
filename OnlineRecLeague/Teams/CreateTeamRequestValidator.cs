using OnlineRecLeague.CommonDataTypes;
using OnlineRecLeague.Users;

namespace OnlineRecLeague.Teams
{
	internal interface ICreateTeamRequestValidator
	{
		Result IsValid(CreateTeamRequest request, IUser loggedInUser);
	}

	internal class CreateTeamRequestValidator : ICreateTeamRequestValidator
	{
		public Result IsValid(CreateTeamRequest request, IUser loggedInUser)
		{
			if (string.IsNullOrWhiteSpace(request.Name))
			{
				return Result.Failure("New team must have a name");
			}

			return Result.Successful();
		}
	}
}
