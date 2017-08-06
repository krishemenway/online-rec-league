using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueService.Teams
{
	public interface ITeamProfile
	{

	}

	public class TeamProfileFactory
	{
		public ITeamProfile Create(ITeam team)
		{
			throw new NotImplementedException();
		}
	}
}
