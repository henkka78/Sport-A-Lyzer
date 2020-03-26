using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sport_A_Lyzer.GameEventOperations;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.GoalOperations
{
	public class UpsertGoalRequest
	{
		public int GoalTypeId { get; set; }
		public Guid PlayerId { get; set; }
		public Guid GameId { get; set; }
		public Guid TeamId { get; set; }
		public int MinuteOfGame { get; set; }
		public ICollection<GameEvents> Assists { get; set; }
	}
}
