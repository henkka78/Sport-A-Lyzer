using Microsoft.AspNetCore.Mvc;
using Sport_A_Lyzer.CQRS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sport_A_Lyzer.CQRS.PlayerOperations;
using Sport_A_Lyzer.CQRS.TeamOperations;

namespace Sport_A_Lyzer.Controllers
{
	[Authorize]
	[ApiController]
    public class TeamsController : ControllerBase
    {
	    private readonly ICommandHandler<UpsertTeamCommand> _upsertTeamCommandHandler;
	    private readonly IQueryHandler<GetTeamsPlayersQuery, ICollection<PlayerResponse>> _getTeamsPlayersQueryHandler;
	    private readonly IQueryHandler<GetTeamsQuery, ICollection<TeamResponse>> _getTeamsQueryHandler;

	    public TeamsController(
		    ICommandHandler<UpsertTeamCommand> upsertTeamCommandHandler,
			IQueryHandler<GetTeamsPlayersQuery, ICollection<PlayerResponse>> getTeamsPlayersQueryHandler,
			IQueryHandler<GetTeamsQuery, ICollection<TeamResponse>> getTeamsQueryHandler
		    )
	    {
		    _upsertTeamCommandHandler = upsertTeamCommandHandler;
		    _getTeamsPlayersQueryHandler = getTeamsPlayersQueryHandler;
		    _getTeamsQueryHandler = getTeamsQueryHandler;
	    }

	    [Route( "api/teams" )]
	    [HttpGet]
	    public async Task<ActionResult<ICollection<TeamResponse>>> GetAsync()
	    {
		    var query = new GetTeamsQuery();
		    var teams = await _getTeamsQueryHandler.HandleAsync( query );
		    return Ok( teams );
	    }

		[Route( "api/teams/{teamId}" )]
		[HttpPut]
		public async Task<ActionResult> PutUpsertTeamAsync( Guid teamId, [FromBody]UpsertTeamRequest request )
		{
			var command=new UpsertTeamCommand(teamId, request.Name);
			await _upsertTeamCommandHandler.HandleAsync(command);
			return Ok();
		}

		[Route( "api/teams/{teamId}/players" )]
		[HttpGet]
		public async Task<ActionResult<ICollection<PlayerResponse>>> GetTeamsPlayersAsync( Guid teamId)
		{
			var query=new GetTeamsPlayersQuery(teamId);
			var players = await _getTeamsPlayersQueryHandler.HandleAsync(query);
			return Ok(players);
		}
	}
}