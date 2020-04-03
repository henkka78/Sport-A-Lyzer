using System;

namespace Sport_A_Lyzer.CQRS.PlayerOperations
{
	public class UpsertPlayerRequest
	{
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public int Number { get; set; }
		public Guid TeamId { get; set; }
	}
}
