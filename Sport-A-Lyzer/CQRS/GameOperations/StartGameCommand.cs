using System;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class StartGameCommand
	{
		public StartGameCommand(Guid gameId, DateTime startTime)
		{
			GameId = gameId;
			StartTime = startTime;
		}
		public Guid GameId { get; set; }
		public DateTime StartTime { get; set; }
	}
}
