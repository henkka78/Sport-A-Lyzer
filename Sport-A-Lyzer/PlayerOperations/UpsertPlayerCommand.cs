using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.PlayerOperations
{
	public class UpsertPlayerCommand
	{
		public UpsertPlayerCommand(Guid playerId, UpsertPlayerRequest upsertPlayerRequest)
		{
			PlayerId = playerId;
			UpsertPlayerRequest = upsertPlayerRequest;
		}
		public Guid PlayerId { get; set; }
		public UpsertPlayerRequest UpsertPlayerRequest { get; set; }
	}
}
