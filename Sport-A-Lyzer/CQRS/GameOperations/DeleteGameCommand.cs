using System;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class DeleteGameCommand
	{
		public DeleteGameCommand(Guid gameId)
		{
			GameId = gameId;
		}
		public Guid GameId { get; set; }
	}
}
