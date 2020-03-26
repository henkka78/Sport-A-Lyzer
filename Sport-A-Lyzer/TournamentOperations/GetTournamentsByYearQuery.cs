using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.TournamentOperations
{
	public class GetTournamentsByYearQuery
	{
		public GetTournamentsByYearQuery(int year)
		{
			Year = year;
		}

		public int Year { get; set; }
	}
}
