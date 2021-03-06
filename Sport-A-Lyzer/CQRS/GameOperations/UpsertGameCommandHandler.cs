﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Helpers;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	internal class UpsertGameCommandHandler : ICommandHandler<UpsertGameCommand>
	{
		private readonly SportALyzerAppDbContext _context;

		public UpsertGameCommandHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}

		public async Task HandleAsync( UpsertGameCommand command )
		{
			var game = await GetOrCreateGame( command.GameId );
			game.TournamentId = command.Request.TournamentId;
			game.AwayTeamId = command.Request.AwayTeamId;
			game.HomeTeamId = command.Request.HomeTeamId;
			game.StartTime = command.Request.StartTime != null
				? LocalTimeProvider.GetLocalTime( command.Request.StartTime.Value )
				: ( DateTime? )null;
			game.GameDay = command.Request.GameDay != null
				? LocalTimeProvider.GetLocalTime(command.Request.GameDay.Value)
				: (DateTime?) null;
			game.PlannedLength = command.Request.PlannedLength;

			await _context.SaveChangesAsync();
		}

		private async Task<Game> GetOrCreateGame( Guid gameId )
		{
			var game = await _context.Game.SingleOrDefaultAsync( g => g.Id == gameId );

			if ( game != null )
			{
				return game;
			}

			game = new Game
			{
				Id = gameId
			};

			_context.Game.Add(game);

			return game;
		}
	}
}
