using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.Models
{
	public partial class Game
	{
		public void StartGame( DateTime timeStamp )
		{
			this.ActualStartTime = timeStamp;
		}

		public void EndGame( DateTime timeStamp )
		{
			this.ActualEndTime = timeStamp;
		}
	}
}
