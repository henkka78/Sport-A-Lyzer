using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.TeamOperations
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
