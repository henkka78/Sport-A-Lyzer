using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class GetGamesRequest
	{
		public int Year { get; set; }
		public int Month { get; set; }
	}
}
