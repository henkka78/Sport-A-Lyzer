using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.GoalOperations
{
	public class GetGamesGoalStatsQuery
	{
		public GetGamesGoalStatsQuery(Guid gameId)
		{
			GameId = gameId;
		}

		public Guid GameId { get; set; }
	}
}
