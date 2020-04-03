using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	internal class GetGamesByTournamentIdQueryHandler : IQueryHandler<GetGamesByTournamentIdQuery, ICollection<GameResponse>>
	{
		private readonly SportALyzerAppDbContext _context;

		public GetGamesByTournamentIdQueryHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}

		public async Task<ICollection<GameResponse>> HandleAsync( GetGamesByTournamentIdQuery byTournamentIdQuery )
		{
			return await _context.Game
				.Where( g => g.TournamentId == byTournamentIdQuery.TournamentId )
				.Include( g => g.AwayTeam )
				.Include( g => g.HomeTeam )
				.Select( g => new GameResponse()
				{
					Id = g.Id,
					AwayTeamName = g.AwayTeam.Name,
					AwayTeamId = g.AwayTeamId,
					HomeTeamName = g.HomeTeam.Name,
					HomeTeamId = g.HomeTeamId
				} ).ToListAsync();
		}
	}
}
