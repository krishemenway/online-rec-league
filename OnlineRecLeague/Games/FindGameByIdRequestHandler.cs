using OnlineRecLeague.CommonDataTypes;
using System;

namespace OnlineRecLeague.Games
{
	public interface IFindGameByIdRequestHandler
	{
		Result<GameProfile> HandleRequest(FindGameByIdRequest request);
	}

	public class FindGameByIdRequestHandler : IFindGameByIdRequestHandler
	{
		public Result<GameProfile> HandleRequest(FindGameByIdRequest request)
		{
			throw new NotImplementedException();
		}
	}
}