using System;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class GetGameQuery
	{
		public GetGameQuery(Guid gameId)
		{
			GameId = gameId;
		}
		public Guid GameId { get; set; }
	}
}
