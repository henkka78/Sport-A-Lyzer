using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
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
			var result= await _context.Game
				.Where(g => g.Id == query.GameId)
				.Include(g => g.AwayTeam)
				.Include(g => g.HomeTeam)
				.Select(g => new GameResponse()
				{
					Id = g.Id,
					HomeTeamName = g.HomeTeam.Name,
					AwayTeamName = g.AwayTeam.Name
				}).FirstOrDefaultAsync();

			return result;
		}
	}
}
