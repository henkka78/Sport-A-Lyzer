using System.Threading.Tasks;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	internal class PauseGameCommandHandler : ICommandHandler<PauseGameCommand>
	{
		private readonly SportALyzerAppDbContext _context;

		public PauseGameCommandHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}

		public async Task HandleAsync( PauseGameCommand command )
		{
			var clockEvent = new GameClockEvent()
			{
				GameId = command.GameId,
				IsClockRunning = false,
				TimeStamp = command.EventTimeStamp,
				SecondsSinceStart = ( int )( command.EventTimeStamp - command.GameStartTime ).TotalSeconds
			};

			_context.GameClockEvent.Add( clockEvent );

			await _context.SaveChangesAsync();
		}
	}
}
