using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.PlayerOperations
{
	public class UpsertPlayerRequest
	{
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public int Number { get; set; }
		public Guid TeamId { get; set; }
	}
}
