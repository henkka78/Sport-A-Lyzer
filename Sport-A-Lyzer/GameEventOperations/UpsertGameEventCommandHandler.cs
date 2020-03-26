using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.GameEventOperations
{
	internal class UpsertGameEventCommandHandler : ICommandHandler<UpsertGameEventCommand>
	{
		private readonly SportALyzerAppDbContext _context;

		public UpsertGameEventCommandHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}
		public async Task HandleAsync( UpsertGameEventCommand command )
		{
			var gameEvent = await GetOrCreateGameEvent( command.EventId );

			gameEvent.TeamId = command.UpsertGameEventRequest.TeamId;
			gameEvent.PlayerId = command.UpsertGameEventRequest.PlayerId;
			gameEvent.EventTypeId = command.UpsertGameEventRequest.EventTypeId;
			gameEvent.TimeStamp = command.UpsertGameEventRequest.TimeStamp;
			gameEvent.Description = command.UpsertGameEventRequest.Description;
			gameEvent.GameId = command.UpsertGameEventRequest.GameId;

			await _context.SaveChangesAsync();

		}

		private async Task<GameEvents> GetOrCreateGameEvent( Guid eventId )
		{
			var gameEvent = await _context.GameEvents.SingleOrDefaultAsync( ge => ge.Id == eventId );

			if ( gameEvent != null )
			{
				return gameEvent;
			}

			gameEvent = new GameEvents()
			{
				Id = eventId
			};

			_context.GameEvents.Add( gameEvent );

			return gameEvent;
		}
	}
}
