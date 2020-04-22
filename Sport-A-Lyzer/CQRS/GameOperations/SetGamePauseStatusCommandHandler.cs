using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Helpers;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	internal class SetGamePauseStatusCommandHandler : ICommandHandler<SetGamePauseStatusCommand>
	{
		private readonly SportALyzerAppDbContext _context;

		public SetGamePauseStatusCommandHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}

		public async Task HandleAsync( SetGamePauseStatusCommand command )
		{
			var game = await _context.Game.SingleOrDefaultAsync( g => g.Id == command.GameId && g.ActualEndTime == null );

			if ( game == null )
			{
				throw new InvalidOperationException( "Antamallasi ID:llä ei löydy aktiivista peliä. Et voi asettaa taukoja!" );
			}

			var pause = await GetOrCreatePause( command.GameId, command.TimeStamp );

			if ( command.IsActivePause )
			{
				pause.EndTime = LocalTimeProvider.GetLocalTime( command.TimeStamp );
			}

			await _context.SaveChangesAsync();
		}

		private async Task<GamePause> GetOrCreatePause( Guid gameId, DateTime timeStamp )
		{
			var pause = await _context.GamePause
				.SingleOrDefaultAsync( gp => gp.GameId == gameId && gp.EndTime == null );

			if ( pause != null )
			{
				return pause;
			}

			pause = new GamePause()
			{
				GameId = gameId,
				StartTime = LocalTimeProvider.GetLocalTime( timeStamp )
			};

			_context.GamePause.Add( pause );
			return pause;
		}
	}
}
