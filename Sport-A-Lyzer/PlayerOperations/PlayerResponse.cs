using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.PlayerOperations
{
	public class PlayerResponse
	{
		public Guid PlayerId { get; set; }
		public string PlayerName { get; set; }
		public int PlayerNumber { get; set; }
		public string Team { get; set; }
	}
}
