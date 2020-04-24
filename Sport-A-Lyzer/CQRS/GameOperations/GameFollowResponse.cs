using System;
using System.Collections.Generic;
using Sport_A_Lyzer.CQRS.GoalOperations;
using Sport_A_Lyzer.CQRS.PlayerOperations;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class GameFollowResponse : GameResponse
	{
		public int SecondsPlayed { get; set; }
		public bool IsPaused { get; set; }
		public int PausesHeld { get; set; }
		public ICollection<GoalStatsResponse> GoalStats { get; set; }
		public Dictionary<string, ICollection<PlayerResponse>> Players { get; set; }
	}
}
