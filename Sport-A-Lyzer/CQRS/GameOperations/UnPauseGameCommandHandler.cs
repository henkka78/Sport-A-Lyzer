using System.Threading.Tasks;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	public class UnPauseGameCommandHandler:ICommandHandler<UnPauseGameCommand>
	{
		private readonly SportALyzerAppDbContext _context;

		public UnPauseGameCommandHandler(SportALyzerAppDbContext context)
		{
			_context = context;
		}

		public async Task HandleAsync(UnPauseGameCommand command)
		{
			var clockEvent = new GameClockEvent()
			{
				GameId = command.GameId,
				IsClockRunning = true,
				TimeStamp = command.EventTimeStamp,
				SecondsSinceStart = ( int )( command.EventTimeStamp - command.GameStartTime ).TotalSeconds
			};

			_context.GameClockEvent.Add( clockEvent );

			await _context.SaveChangesAsync();
		}
	}
}
