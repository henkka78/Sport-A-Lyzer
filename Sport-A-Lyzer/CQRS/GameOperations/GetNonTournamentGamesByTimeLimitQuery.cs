using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class GetNonTournamentGamesByTimeLimitQuery
	{
		public GetNonTournamentGamesByTimeLimitQuery(int year, int month)
		{
			Year = year;
			Month = month;
		}
		public int Year { get; set; }
		public int Month { get; set; }
	}
}
