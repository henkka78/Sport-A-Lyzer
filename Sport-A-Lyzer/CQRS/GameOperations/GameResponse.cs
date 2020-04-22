using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class GameResponse
	{
		public Guid Id { get; set; }
		public Guid HomeTeamId { get; set; }
		public string HomeTeamName { get; set; }
		public Guid AwayTeamId { get; set; }
		public string AwayTeamName { get; set; }
		public DateTime? GameDay { get; set; }
		public DateTime? StartTime { get; set; }
		public string PitchName { get; set; }
		public string Description { get; set; }
		public int? PlannedLength { get; set; }
		public string Result { get; set; }
		public DateTime? ActualStartTime { get; set; }
		public DateTime? ActualEndTime { get; set; }
	}
}
