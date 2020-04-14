using System;

namespace Sport_A_Lyzer.CQRS.GameOperations
{

	public class EndGameCommand
	{
		public EndGameCommand(Guid gameId, DateTime endTime)
		{
			GameId = gameId;
			EndTime = endTime;
		}
		public Guid GameId { get; set; }
		public DateTime EndTime { get; set; }

	}
}
