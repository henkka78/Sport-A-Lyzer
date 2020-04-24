using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.PlayerOperations
{
	internal class GetTeamsPlayersQueryHandler : IQueryHandler<GetTeamsPlayersQuery, ICollection<PlayerResponse>>
	{
		private readonly SportALyzerAppDbContext _context;

		public GetTeamsPlayersQueryHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}

		public async Task<ICollection<PlayerResponse>> HandleAsync( GetTeamsPlayersQuery query )
		{
			return await _context.Player
				.Where( p => p.TeamId == query.TeamId )
				.Include( p => p.Team )
				.Select( p => new PlayerResponse()
				{
					PlayerId = p.Id,
					FirstName = p.FirstName,
					LastName = p.LastName,
					PlayerNumber = p.Number,
					Team = p.Team.Name
				} )
				.OrderBy( p => p.PlayerNumber )
				.ToListAsync();
		}
	}
}
