using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.TeamOperations
{
	internal class GetTeamsQueryHandler : IQueryHandler<GetTeamsQuery, ICollection<TeamResponse>>
	{
		private readonly SportALyzerAppDbContext _context;

		public GetTeamsQueryHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}
		public async Task<ICollection<TeamResponse>> HandleAsync( GetTeamsQuery query )
		{
			return await _context.Team.Select(t => new TeamResponse()
			{
				TeamId = t.Id,
				Name = t.Name
			}).ToListAsync();
		}
	}
}
