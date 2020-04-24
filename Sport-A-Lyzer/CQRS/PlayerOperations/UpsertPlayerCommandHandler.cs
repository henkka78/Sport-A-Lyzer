using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.PlayerOperations
{
	internal class UpsertPlayerCommandHandler:ICommandHandler<UpsertPlayerCommand>
	{
		private readonly SportALyzerAppDbContext _context;

		public UpsertPlayerCommandHandler(SportALyzerAppDbContext context)
		{
			_context = context;
		}
		public async Task HandleAsync(UpsertPlayerCommand command)
		{
			var player = await GetOrCreatePlayer(command.PlayerId);

			player.TeamId = command.UpsertPlayerRequest.TeamId;
			player.Number = command.UpsertPlayerRequest.Number;
			player.LastName = command.UpsertPlayerRequest.LastName;
			player.FirstName = command.UpsertPlayerRequest.FirstName;

			await _context.SaveChangesAsync();
		}

		private async Task<Player> GetOrCreatePlayer(Guid playerId)
		{
			var player = await _context.Player.SingleOrDefaultAsync(p => p.Id == playerId);

			if (player != null)
			{
				return player;
			}

			player=new Player()
			{
				Id = playerId
			};

			_context.Player.Add(player);

			return player;
		}
	}
}
