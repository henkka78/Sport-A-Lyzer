using System;

namespace Sport_A_Lyzer.CQRS.PlayerOperations
{
	public class PlayerResponse
	{
		public Guid PlayerId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int PlayerNumber { get; set; }
		public string Team { get; set; }
	}
}
