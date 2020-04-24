using System;

namespace Sport_A_Lyzer.Models
{
	public partial class GamePause
	{
		public bool IsActivePause => this.EndTime == null;
		public void StarPause( DateTime timeStamp )
		{
			this.StartTime = timeStamp.ToLocalTime();
		}

		public void EndPause( DateTime timeStamp )
		{
			this.EndTime = timeStamp.ToLocalTime();
		}
	}
}
