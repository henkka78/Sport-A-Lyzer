using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	internal class DeleteGameCommandHandler:ICommandHandler<DeleteGameCommand>
	{
		private readonly SportALyzerAppDbContext _context;

		public DeleteGameCommandHandler(SportALyzerAppDbContext context)
		{
			_context = context;
		}

		public async Task HandleAsync(DeleteGameCommand command)
		{
			var gameToDelete = await _context.Game
				.FirstOrDefaultAsync(g => g.Id == command.GameId);

			if (gameToDelete == null)
			{
				return;
			}

			_context.Game.Remove(gameToDelete);

			await _context.SaveChangesAsync();
		}
	}
}
