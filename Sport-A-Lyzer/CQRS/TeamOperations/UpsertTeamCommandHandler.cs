using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.TeamOperations
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

		private async Task<Team> GetOrCreateTeam( Guid teamId )
		{
			var team = await _context.Team.SingleOrDefaultAsync( t => t.Id == teamId );

			if ( team != null )
			{
				return team;
			}

			team= new Team()
			{
				Id = teamId
			};

			_context.Team.Add(team);

			return team;
		}
	}
}
