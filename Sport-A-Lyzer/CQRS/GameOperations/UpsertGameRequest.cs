using System;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class UpsertGameRequest
	{
		public Guid? TournamentId { get; set; }
		public Guid HomeTeamId { get; set; }
		public Guid AwayTeamId { get; set; }
	}
}
