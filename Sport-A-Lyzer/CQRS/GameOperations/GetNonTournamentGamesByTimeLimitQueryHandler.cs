using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	internal class GetNonTournamentGamesByTimeLimitQueryHandler:IQueryHandler<GetNonTournamentGamesByTimeLimitQuery, ICollection<GameResponse>>
	{
		private readonly SportALyzerAppDbContext _context;

		public GetNonTournamentGamesByTimeLimitQueryHandler(
			SportALyzerAppDbContext context
			)
		{
			_context = context;
		}
		public async Task<ICollection<GameResponse>> HandleAsync(GetNonTournamentGamesByTimeLimitQuery query)
		{
			return await _context.Game
				.Where(g => g.TournamentId == null && 
				            g.GameDay.Value.Year == query.Year &&
				            g.GameDay.Value.Month == query.Month)
				.Include( g => g.AwayTeam )
				.Include( g => g.HomeTeam )
				.Include( g => g.Goal )
				.OrderBy( g => g.GameDay )
				.ThenBy( g => g.StartTime )
				.Select( g => new GameResponse()
				{
					Id = g.Id,
					AwayTeamName = g.AwayTeam.Name,
					AwayTeamId = g.AwayTeamId,
					HomeTeamName = g.HomeTeam.Name,
					HomeTeamId = g.HomeTeamId,
					GameDay = g.GameDay,
					StartTime = g.StartTime,
					PitchName = g.PitchName,
					Description = g.Description,
				} ).ToListAsync();
		}
	}
}
