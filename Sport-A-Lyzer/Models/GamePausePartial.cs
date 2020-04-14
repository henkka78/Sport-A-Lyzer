using System;

namespace Sport_A_Lyzer.Models
{
	public partial class GamePause
	{
		public bool IsActivePause => this.StartTime != null && this.EndTime == null;
		public void StarPause(DateTime timeStamp)
		{
			this.StartTime = timeStamp;
		}

		public void EndPause(DateTime timeStamp)
		{
			this.EndTime = timeStamp;
		}
	}
}
