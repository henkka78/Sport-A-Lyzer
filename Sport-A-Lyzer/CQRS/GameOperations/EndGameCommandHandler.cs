using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	internal class EndGameCommandHandler : ICommandHandler<EndGameCommand>
	{
		private readonly SportALyzerAppDbContext _context;

		public EndGameCommandHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}
		public async Task HandleAsync( EndGameCommand command )
		{
			var game = await _context.Game.SingleOrDefaultAsync( g =>
				 g.Id == command.GameId && g.ActualStartTime != null && g.ActualEndTime == null );

			if ( game == null )
			{
				throw new InvalidOperationException( "Antamallasi ID:llä ei löydy käynnissä olevaa peliä!" );
			}

			game.EndGame( command.EndTime );
			await _context.SaveChangesAsync();
		}
	}
}
