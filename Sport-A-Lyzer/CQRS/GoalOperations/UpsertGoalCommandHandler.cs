using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Models;
using Sport_A_Lyzer.Services;

namespace Sport_A_Lyzer.CQRS.GoalOperations
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
			//var assists = await GetOrCreateAssists(command.GoalId, command.UpsertGoalRequest.Assists);

			goal.GoalTypeId = command.UpsertGoalRequest.GoalTypeId;
			goal.PlayerId = command.UpsertGoalRequest.PlayerId;
			goal.GameId = command.UpsertGoalRequest.GameId;
			goal.TeamId = command.UpsertGoalRequest.TeamId;
			goal.MinuteOfGame = command.UpsertGoalRequest.MinuteOfGame;

			//foreach ( var assist in assists )
			//{
			//	var currentAssist = command.UpsertGoalRequest.Assists.Single( a => a.AssistId == assist.Id );
			//	assist.EventTypeId = new Guid( DatabaseConstants.GameEventTypeId.Assist.Name );
			//	assist.GameId = command.UpsertGoalRequest.GameId;
			//	assist.PlayerId = currentAssist.PlayerId;
			//	assist.TeamId = command.UpsertGoalRequest.TeamId;
			//}

			await _context.SaveChangesAsync();
		}

		private async Task<Goal> GetOrCreateGoal( Guid goalId )
		{
			var goal = await _context.Goal.SingleOrDefaultAsync( g => g.Id == goalId );

			if ( goal != null )
			{
				return goal;
			}

			goal = new Goal()
			{
				Id = goalId
			};

			_context.Goal.Add( goal );

			return goal;
		}

		//private async Task<ICollection<GameEvent>> GetOrCreateAssists( Guid goalId, ICollection<AssistRequest> assists )
		//{
		//	var assistIds = assists.Select( a => a.AssistId ).ToList();

		//	var dbAssists = await _context.GameEvent
		//		.Where( ge => assistIds.Contains( ge.Id ) )
		//		.ToListAsync();

		//	var assistResults = new List<GameEvent>();

		//	foreach (var assistId in assistIds)
		//	{
		//		if (dbAssists.Any(a => a.Id == assistId))
		//		{
		//			assistResults.Add(dbAssists.Single(a=>a.Id==assistId));
		//		}
		//		else
		//		{
		//			var assist=new GameEvent()
		//			{
		//				Id=assistId
		//			};

		//			assistResults.Add(assist);
		//			_context.GameEvent.Add(assist);

		//		}
		//	}

		//	return assistResults;

		//}
	}
}
