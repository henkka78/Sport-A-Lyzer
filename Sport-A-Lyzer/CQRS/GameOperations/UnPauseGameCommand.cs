using System;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class UnPauseGameCommand
	{
		public UnPauseGameCommand( Guid gameId, DateTime eventTimeStamp, DateTime gameStartTime )
		{
			GameId = gameId;
			EventTimeStamp = eventTimeStamp;
			GameStartTime = gameStartTime;
		}

		public Guid GameId { get; set; }
		public DateTime EventTimeStamp { get; set; }
		public DateTime GameStartTime { get; set; }
	}
}
