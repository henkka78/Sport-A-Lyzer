using System;

namespace Sport_A_Lyzer.CQRS.GoalOperations
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
