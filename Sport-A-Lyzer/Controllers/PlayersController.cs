using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.CQRS.PlayerOperations;

namespace Sport_A_Lyzer.Controllers
{
	[Authorize]
	[ApiController]
    public class PlayersController : ControllerBase
    {
	    private readonly ICommandHandler<UpsertPlayerCommand> _upsertPlayerCommandHandler;

	    public PlayersController(
		    ICommandHandler<UpsertPlayerCommand> upsertPlayerCommandHandler
		    )
	    {
		    _upsertPlayerCommandHandler = upsertPlayerCommandHandler;
	    }

		[Route( "api/players/{playerId}" )]
		[HttpPost]
		public async Task<ActionResult> PostUpsertPlayerAsync( Guid playerId, [FromBody]UpsertPlayerRequest request )
		{
			var command = new UpsertPlayerCommand( playerId , request);
			await _upsertPlayerCommandHandler.HandleAsync(command);
			return Ok();
		}
	}
}