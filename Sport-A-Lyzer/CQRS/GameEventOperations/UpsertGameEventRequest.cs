using System;

namespace Sport_A_Lyzer.CQRS.GameEventOperations
{
	public class UpsertGameEventRequest
	{
		public Guid EventTypeId { get; set; }
		public Guid PlayerId { get; set; }
		public Guid? TeamId { get; set; }
		public DateTime TimeStamp { get; set; }
		public Guid GameId { get; set; }
		public string Description { get; set; }
	}
}
