using System;

namespace Sport_A_Lyzer.CQRS.TeamOperations
{
	public class UpsertTeamCommand
	{
		public UpsertTeamCommand(Guid teamId, string name)
		{
			TeamId = teamId;
			Name = name;
		}
		public Guid TeamId { get; set; }
		public string Name { get; set; }
	}
}
