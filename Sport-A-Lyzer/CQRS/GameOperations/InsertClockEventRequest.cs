using System;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class InsertClockEventRequest
	{
		public DateTime EventTimeStamp { get; set; }
		public DateTime GameStartTime { get; set; }
	}
}
