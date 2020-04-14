using System;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class SetGamePauseStatusCommand
	{
		public SetGamePauseStatusCommand( Guid gameId, DateTime timeStamp )
		{
			GameId = gameId;
			TimeStamp = timeStamp;
		}

		public Guid GameId { get; set; }
		public DateTime TimeStamp { get; set; }

	}
}
