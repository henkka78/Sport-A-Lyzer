using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.CQRS.GoalOperations
{
	public class GoalStatsResponse
	{
		public Guid TeamId { get; set; }
		public string TeamName { get; set; }
		public ICollection<Scorer> Scorers { get; set; }
	}

	public class Scorer
	{
		public string Name { get; set; }
		public int Number { get; set; }
		public int NumberOfGoals { get; set; }
		public ICollection<int> Minutes { get; set; }
	}
}
