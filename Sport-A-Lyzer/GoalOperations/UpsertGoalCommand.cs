using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.GoalOperations
{
	public class UpsertGoalCommand
	{
		public UpsertGoalCommand(Guid goalId, UpsertGoalRequest upsertGoalRequest)
		{
			GoalId = goalId;
			UpsertGoalRequest = upsertGoalRequest;
		}

		public Guid GoalId { get; set; }
		public UpsertGoalRequest UpsertGoalRequest { get; set; }
	}
}
