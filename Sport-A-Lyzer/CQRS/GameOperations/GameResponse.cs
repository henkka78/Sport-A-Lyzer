using System;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class GameResponse
	{
		public Guid Id { get; set; }
		public Guid HomeTeamId { get; set; }
		public string HomeTeamName { get; set; }
		public Guid AwayTeamId { get; set; }
		public string AwayTeamName { get; set; }
	}
}
