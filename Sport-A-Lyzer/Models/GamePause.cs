using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
	public partial class GamePause
	{
		public int Id { get; set; }
		public Guid GameId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime? EndTime { get; set; }

		public virtual Game Game { get; set; }
	}
}
