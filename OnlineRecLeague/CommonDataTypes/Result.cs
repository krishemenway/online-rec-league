using System.Collections.Generic;

namespace OnlineRecLeague.CommonDataTypes
{
	public class Result
	{
		public bool Success { get; set; }
		public IReadOnlyList<string> Errors { get; set; }

		public static Result Successful()
		{
			return new Result
			{
				Success = true,
				Errors = new string[0]
			};
		}

		public static Result Failure(params string[] errors)
		{
			return new Result
			{
				Success = false,
				Errors = errors
			};
		}
	}

	public class Result<T>
	{
		public bool Success { get; set; }
		public T Value { get; set; }
		public IReadOnlyList<string> Errors { get; set; }

		public static Result<T> Successful(T value)
		{
			return new Result<T>
			{
				Success = true,
				Value = value,
				Errors = new string[0]
			};
		}

		public static Result<T> Failure(params string[] errors)
		{
			return new Result<T>
			{
				Success = false,
				Value = default(T),
				Errors = errors
			};
		}
	}
}
