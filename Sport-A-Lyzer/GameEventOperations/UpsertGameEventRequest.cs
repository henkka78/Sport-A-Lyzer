using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.GameEventOperations
{
	public class UpsertGameEventRequest
	{
		public int EventTypeId { get; set; }
		public Guid PlayerId { get; set; }
		public Guid? TeamId { get; set; }
		public DateTime TimeStamp { get; set; }
		public Guid GameId { get; set; }
		public string Description { get; set; }
	}
}
