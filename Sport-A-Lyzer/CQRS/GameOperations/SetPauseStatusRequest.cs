using System;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class SetPauseStatusRequest
	{
		public DateTime EventTimeStamp { get; set; }
	}
}
