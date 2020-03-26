using Microsoft.AspNetCore.Mvc;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.TeamOperations;
using System;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.Controllers
{
	[ApiController]
    public class TeamsController : ControllerBase
    {
	    private readonly ICommandHandler<UpsertTeamCommand> _upsertTeamCommandHandler;

	    public TeamsController(
		    ICommandHandler<UpsertTeamCommand> upsertTeamCommandHandler
		    )
	    {
		    _upsertTeamCommandHandler = upsertTeamCommandHandler;
	    }

		[Route("teams/{teamId}")]
		[HttpPost]
		public async Task<ActionResult> PostUpsertTeamAsync( Guid teamId, [FromBody]UpsertTeamRequest request )
		{
			var command=new UpsertTeamCommand(teamId, request.Name);
			await _upsertTeamCommandHandler.HandleAsync(command);
			return Ok();
		}
	}
}