using System;

namespace Sport_A_Lyzer.CQRS.PlayerOperations
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
