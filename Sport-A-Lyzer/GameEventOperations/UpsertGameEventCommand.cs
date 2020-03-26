using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.GameEventOperations
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
