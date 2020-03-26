using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.GameEventOperations;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.GoalOperations
{
	internal class UpsertGoalCommandHandler : ICommandHandler<UpsertGoalCommand>
	{
		private readonly SportALyzerAppDbContext _context;

		public UpsertGoalCommandHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}

		public async Task HandleAsync( UpsertGoalCommand command )
		{
			var goal =await GetOrCreateGoal( command.GoalId );
			var assists = await GetOrCreateAssists(command.GoalId, command.UpsertGoalRequest.Assists);

			goal.GoalTypeId = command.UpsertGoalRequest.GoalTypeId;
			goal.PlayerId = command.UpsertGoalRequest.PlayerId;
			goal.GameId = command.UpsertGoalRequest.GameId;
			goal.TeamId = command.UpsertGoalRequest.TeamId;
			goal.MinuteOfGame = command.UpsertGoalRequest.MinuteOfGame;

			foreach (var assist in assists)
			{
				var currentAssist = command.UpsertGoalRequest.Assists.Single(a => a.Id == assist.Id);
				assist.EventTypeId = currentAssist.EventTypeId;
				assist.GameId = currentAssist.GameId;
				assist.PlayerId = currentAssist.PlayerId;
				assist.Description = currentAssist.Description;
				assist.TeamId = currentAssist.TeamId;
			}

			await _context.SaveChangesAsync();
		}

		private async Task<Goals> GetOrCreateGoal( Guid goalId )
		{
			var goal = await _context.Goals.SingleOrDefaultAsync( g => g.Id == goalId );

			if ( goal != null )
			{
				return goal;
			}

			goal = new Goals()
			{
				Id = goalId
			};

			_context.Goals.Add( goal );

			return goal;
		}

		private async Task<ICollection<GameEvents>> GetOrCreateAssists( Guid goalId, ICollection<GameEvents> assists )
		{
			var assistIds = assists.Select( a => a.Id ).ToList();

			var dbAssists = await _context.GameEvents
				.Where( ge => assistIds.Contains( ge.Id ) )
				.ToListAsync();

			var assistResults = new List<GameEvents>();

			foreach (var assistId in assistIds)
			{
				if (dbAssists.Any(a => a.Id == assistId))
				{
					assistResults.Add(dbAssists.Single(a=>a.Id==assistId));
				}
				else
				{
					var assist=new GameEvents()
					{
						Id=assistId
					};

					assistResults.Add(assist);
					_context.GameEvents.Add(assist);

					_context.GoalsEvents.Add(new GoalsEvents()
					{
						GoalId = goalId,
						GameEventId = assistId
					});
				}
			}

			return assistResults;

		}
	}
}
