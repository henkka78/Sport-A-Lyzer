using System;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class SetGamePauseStatusCommand
	{
		public SetGamePauseStatusCommand( Guid gameId, DateTime timeStamp, bool isActivePause )
		{
			GameId = gameId;
			TimeStamp = timeStamp;
			IsActivePause = isActivePause;


		}

		public Guid GameId { get; set; }
		public DateTime TimeStamp { get; set; }
		public bool IsActivePause { get; set; }

	}
}
