using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.GameOperations
{
	public class DeleteGameCommand
	{
		public DeleteGameCommand(Guid gameId)
		{
			GameId = gameId;
		}
		public Guid GameId { get; set; }
	}
}
