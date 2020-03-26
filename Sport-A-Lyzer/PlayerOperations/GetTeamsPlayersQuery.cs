using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.PlayerOperations
{
	public class GetTeamsPlayersQuery
	{
		public GetTeamsPlayersQuery(Guid teamId)
		{
			TeamId = teamId;
		}

		public Guid TeamId { get; set; }
		
	}
}
