using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.GameOperations
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
			var gameToDelete = await _context.Games
				.FirstOrDefaultAsync(g => g.Id == command.GameId);

			if (gameToDelete == null)
			{
				return;
			}

			_context.Games.Remove(gameToDelete);

			await _context.SaveChangesAsync();
		}
	}
}
