namespace Sport_A_Lyzer.CQRS.TournamentOperations
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
