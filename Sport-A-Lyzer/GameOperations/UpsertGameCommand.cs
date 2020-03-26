using System;

namespace Sport_A_Lyzer.GameOperations
{
	public class UpsertGameCommand
	{
		public UpsertGameCommand( Guid gameId, UpsertGameRequest request )
		{
			GameId = gameId;
			Request = request;
		}

		public Guid GameId { get; set; }
		public UpsertGameRequest Request { get; set; }
	}
}
