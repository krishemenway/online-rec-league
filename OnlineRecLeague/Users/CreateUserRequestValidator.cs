using OnlineRecLeague.DataTypes;

namespace OnlineRecLeague.Users
{
	public interface ICreateUserRequestValidator
	{
		Result Validate(CreateUserRequest request);
	}

	public class CreateUserRequestValidator : ICreateUserRequestValidator
	{
		public Result Validate(CreateUserRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.DefaultTimezone))
			{
				return Result.Failure("");
			}

			if (string.IsNullOrWhiteSpace(request.NickName))
			{
				return Result.Failure("");
			}

			if (string.IsNullOrWhiteSpace(request.NickName))
			{
				return Result.Failure("");
			}

			return Result.Successful();
		}
	}
}
