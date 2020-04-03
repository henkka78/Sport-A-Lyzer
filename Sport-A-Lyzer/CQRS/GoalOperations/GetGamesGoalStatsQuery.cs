using System;

namespace Sport_A_Lyzer.CQRS.GoalOperations
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
