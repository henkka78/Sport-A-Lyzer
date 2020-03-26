using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.TournamentOperations
{
	internal class GetTournamentsByYearQueryHandler : IQueryHandler<GetTournamentsByYearQuery, ICollection<TournamentResponse>>
	{
		private readonly SportALyzerAppDbContext _context;

		public GetTournamentsByYearQueryHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}

		public async Task<ICollection<TournamentResponse>> HandleAsync( GetTournamentsByYearQuery query )
		{
			var fromDate = new DateTime( query.Year, 1, 1 );
			var toDate = new DateTime( query.Year, 12, 31 );
			return await _context.Tournaments
				.Where(t => t.StartTime >= fromDate && t.EndTime <= toDate)
				.Select(t => new TournamentResponse()
				{
					Id=t.Id,
					Name = t.Name,
					StartTime = t.StartTime,
					EndTime = t.EndTime
				}).ToListAsync();
		}
	}
}
