using System;
using System.Collections.Generic;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.TournamentOperations
{
	public class TournamentResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public ICollection<Game> Games { get; set; }
	}
}
