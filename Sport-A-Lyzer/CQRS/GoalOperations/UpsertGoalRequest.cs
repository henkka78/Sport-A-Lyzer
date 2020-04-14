using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sport_A_Lyzer.CQRS.GoalOperations
{
	public class UpsertGoalRequest
	{
		public int GoalTypeId { get; set; }
		public Guid PlayerId { get; set; }
		public Guid GameId { get; set; }
		public Guid TeamId { get; set; }
		public int MinuteOfGame { get; set; }
		//[JsonProperty( PropertyName = "assists" )]
		//public ICollection<AssistRequest> Assists { get; set; }
	}

	public class AssistRequest
	{
		public Guid AssistId { get; set; }
		public Guid PlayerId { get; set; }
	}
}
