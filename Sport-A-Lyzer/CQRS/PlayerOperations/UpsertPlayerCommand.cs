using System;

namespace Sport_A_Lyzer.CQRS.PlayerOperations
{
	public class UpsertPlayerCommand
	{
		public UpsertPlayerCommand(Guid playerId, UpsertPlayerRequest upsertPlayerRequest)
		{
			PlayerId = playerId;
			UpsertPlayerRequest = upsertPlayerRequest;
		}
		public Guid PlayerId { get; set; }
		public UpsertPlayerRequest UpsertPlayerRequest { get; set; }
	}
}
