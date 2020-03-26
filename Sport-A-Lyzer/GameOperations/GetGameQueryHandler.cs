using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.GameOperations
{
	internal class GetGameQueryHandler : IQueryHandler<GetGameQuery, GameResponse>
	{
		private readonly SportALyzerAppDbContext _context;

		public GetGameQueryHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}
		public async Task<GameResponse> HandleAsync( GetGameQuery query )
		{
			return await _context.Games
				.Where(g => g.Id == query.GameId)
				.Include(g => g.AwayTeam)
				.Include(g => g.HomeTeam)
				.Select(g => new GameResponse()
				{
					Id = g.Id,
					HomeTeam = g.HomeTeam.Name,
					AwayTeam = g.AwayTeam.Name
				}).FirstOrDefaultAsync();
		}
	}
}
