using OnlineRecLeague.DataTypes;
using System;

namespace OnlineRecLeague.Games
{
	public interface IFindGameByNameRequestHandler
	{
		Result<FindGameByNameResponse> HandleRequest(FindGameByNameRequest request);
	}

	internal class FindGameByNameRequestHandler : IFindGameByNameRequestHandler
	{
		public Result<FindGameByNameResponse> HandleRequest(FindGameByNameRequest request)
		{
			throw new NotImplementedException();
		}
	}
}