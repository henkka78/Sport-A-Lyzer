using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.GoalOperations
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
			var goals = await _context.Goals
				.Include( g => g.Team )
				.Include( g => g.Player )
				.Where( g => g.GameId == query.GameId )
				.ToListAsync();

			var result = goals.GroupBy( g => new { g.TeamId, g.Team.Name } )
				.Select( g => new GoalStatsResponse()
				{
					TeamId = g.Key.TeamId,
					TeamName = g.Key.Name
				} ).ToList();

			foreach ( var team in result )
			{
				team.Scorers = goals.Where( g => g.TeamId == team.TeamId )
					.GroupBy( g => new { g.Player } )
					.Select( g => new Scorer()
					{
						Name = g.Key.Player.FirstName + " " + g.Key.Player.LastName,
						Number = g.Key.Player.Number,
						NumberOfGoals = g.Count(),
						Minutes = goals.Where( gg => gg.PlayerId == g.Key.Player.Id ).Select( gg => gg.MinuteOfGame ).ToList()
					} ).ToList();
			}

			return result;
		}
	}
}
