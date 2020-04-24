using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Helpers;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class StartGameCommandHandler : ICommandHandler<StartGameCommand>
	{
		private readonly SportALyzerAppDbContext _context;

		public StartGameCommandHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}

		public async Task HandleAsync( StartGameCommand command )
		{
			var game = await _context.Game
				.SingleOrDefaultAsync( g => g.Id == command.GameId && g.ActualStartTime == null );

			if ( game == null )
			{
				throw new InvalidOperationException( "Antamallasi ID:llä ei löydy käynnistämätöntä peliä!" );
			}

			game.StartGame( LocalTimeProvider.GetLocalTime( command.StartTime ) );
			await _context.SaveChangesAsync();
		}
	}
}
