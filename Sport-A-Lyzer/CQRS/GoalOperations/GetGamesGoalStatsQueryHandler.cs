using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GoalOperations
{
	internal class GetGamesGoalStatsQueryHandler : IQueryHandler<GetGamesGoalStatsQuery, ICollection<GoalStatsResponse>>
	{
		private readonly SportALyzerAppDbContext _context;

		public GetGamesGoalStatsQueryHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}

		public async Task<ICollection<GoalStatsResponse>> HandleAsync( GetGamesGoalStatsQuery query )
		{
			var goals = await _context.Goal
				.Include( g => g.Team )
				.Include( g => g.Player )
				.Where( g => g.GameId == query.GameId )
				.ToListAsync();

			var result = goals.GroupBy( g => new { g.TeamId, g.Team.Name } )
				.Select( tgr => new GoalStatsResponse()
				{
					TeamId = tgr.Key.TeamId,
					TeamName = tgr.Key.Name,
					Scorers = goals.Where( g => g.TeamId == tgr.Key.TeamId )
						.GroupBy( prp => new { prp.Player } )
						.Select( p => new Scorer()
						{
							Name = p.Key.Player.FirstName + " " + p.Key.Player.LastName,
							Number = p.Key.Player.Number,
							NumberOfGoals = p.Count(),
							Minutes = goals.Where( gg => gg.PlayerId == p.Key.Player.Id ).Select( gg => gg.MinuteOfGame ).ToList()
						} ).ToList()
				} ).ToList();

			return result;
		}
	}
}
