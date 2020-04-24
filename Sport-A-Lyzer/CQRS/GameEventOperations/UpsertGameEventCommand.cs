using System;

namespace Sport_A_Lyzer.CQRS.GameEventOperations
{
	public class UpsertGameEventCommand
	{
		public UpsertGameEventCommand(Guid eventId, UpsertGameEventRequest upsertGameEventRequest)
		{
			EventId = eventId;
			UpsertGameEventRequest = upsertGameEventRequest;
		}
		public Guid EventId { get; set; }
		public UpsertGameEventRequest UpsertGameEventRequest { get; set; }
	}
}
