using System;

namespace Sport_A_Lyzer.GameOperations
{
	public class GetGamesByTournamentIdQuery
	{
		public GetGamesByTournamentIdQuery( Guid tournamentId )
		{
			TournamentId = tournamentId;
		}

		public Guid TournamentId { get; set; }
	}
}
