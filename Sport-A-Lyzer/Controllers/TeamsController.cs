using Microsoft.AspNetCore.Mvc;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.TeamOperations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sport_A_Lyzer.PlayerOperations;

namespace Sport_A_Lyzer.Controllers
{
	[ApiController]
    public class TeamsController : ControllerBase
    {
	    private readonly ICommandHandler<UpsertTeamCommand> _upsertTeamCommandHandler;
	    private readonly IQueryHandler<GetTeamsPlayersQuery, ICollection<PlayerResponse>> _getTeamsPlayersQueryHandler;

	    public TeamsController(
		    ICommandHandler<UpsertTeamCommand> upsertTeamCommandHandler,
			IQueryHandler<GetTeamsPlayersQuery, ICollection<PlayerResponse>> getTeamsPlayersQueryHandler
		    )
	    {
		    _upsertTeamCommandHandler = upsertTeamCommandHandler;
		    _getTeamsPlayersQueryHandler = getTeamsPlayersQueryHandler;
	    }

		[Route("teams/{teamId}")]
		[HttpPost]
		public async Task<ActionResult> PostUpsertTeamAsync( Guid teamId, [FromBody]UpsertTeamRequest request )
		{
			var command=new UpsertTeamCommand(teamId, request.Name);
			await _upsertTeamCommandHandler.HandleAsync(command);
			return Ok();
		}

		[Route( "teams/{teamId}/players" )]
		[HttpGet]
		public async Task<ActionResult<ICollection<PlayerResponse>>> GetTeamsPlayersAsync( Guid teamId)
		{
			var query=new GetTeamsPlayersQuery(teamId);
			var players = await _getTeamsPlayersQueryHandler.HandleAsync(query);
			return Ok(players);
		}
	}
}