using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.TournamentOperations
{
	public class TournamentResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public ICollection<Games> Games { get; set; }
	}
}
