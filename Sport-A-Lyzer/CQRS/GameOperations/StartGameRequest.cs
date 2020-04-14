using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class StartGameRequest
	{
		public DateTime StartTime { get; set; }
	}
}
