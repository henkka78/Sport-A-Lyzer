using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.TeamOperations
{
	internal class UpsertTeamCommandHandler : ICommandHandler<UpsertTeamCommand>
	{
		private readonly SportALyzerAppDbContext _context;

		public UpsertTeamCommandHandler( SportALyzerAppDbContext context )
		{
			_context = context;
		}

		public async Task HandleAsync( UpsertTeamCommand command )
		{
			var team = await GetOrCreateTeam( command.TeamId );
			team.Name = command.Name;
			await _context.SaveChangesAsync();
		}

		private async Task<Teams> GetOrCreateTeam( Guid teamId )
		{
			var team = await _context.Teams.SingleOrDefaultAsync( t => t.Id == teamId );

			if ( team != null )
			{
				return team;
			}

			team= new Teams()
			{
				Id = teamId
			};

			_context.Teams.Add(team);

			return team;
		}
	}
}
