using System;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class UpsertGameRequest
	{
		public Guid? TournamentId { get; set; }
		public Guid HomeTeamId { get; set; }
		public Guid AwayTeamId { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? GameDay { get; set; }
		public string Description { get; set; }
		public string PitchName { get; set; }
		public int PlannedLength { get; set; }
	}
}
