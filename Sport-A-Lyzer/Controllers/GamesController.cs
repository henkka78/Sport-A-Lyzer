using Microsoft.AspNetCore.Mvc;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.GameOperations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sport_A_Lyzer.GoalOperations;

namespace Sport_A_Lyzer.Controllers
{

	[ApiController]
	public class GamesController : ControllerBase
	{
		private readonly ICommandHandler<UpsertGameCommand> _upsertGameCommandHandler;
		private readonly ICommandHandler<DeleteGameCommand> _deleteGameCommandHandler;
		private readonly IQueryHandler<GetGameQuery, GameResponse> _getGameQueryHandler;
		private readonly IQueryHandler<GetGamesGoalStatsQuery, ICollection<GoalStatsResponse>> _getGamesGoalStatsQueryHandler;

		public GamesController( 
			ICommandHandler<UpsertGameCommand> upsertGameCommandHandler,
			ICommandHandler<DeleteGameCommand> deleteGameCommandHandler,
			IQueryHandler<GetGameQuery, GameResponse> getGameQueryHandler,
			IQueryHandler<GetGamesGoalStatsQuery, ICollection<GoalStatsResponse>> getGamesGoalStatsQueryHandler
			)
		{
			_upsertGameCommandHandler = upsertGameCommandHandler;
			_deleteGameCommandHandler = deleteGameCommandHandler;
			_getGameQueryHandler = getGameQueryHandler;
			_getGamesGoalStatsQueryHandler = getGamesGoalStatsQueryHandler;
		}

		[Route("games/{gameId}")]
		[HttpGet]
		public async Task<ActionResult<GameResponse>> GetGameAsync(Guid gameId)
		{
			var query=new GetGameQuery(gameId);
			var game =await _getGameQueryHandler.HandleAsync(query);
			return Ok(game);
		}

		[Route( "games/{gameId}" )]
		[HttpPost]
		public async Task<ActionResult> PostUpsertGameAsync( Guid gameId, [FromBody]UpsertGameRequest request )
		{
			var command=new UpsertGameCommand(gameId, request);
			await _upsertGameCommandHandler.HandleAsync(command);
			return Ok();
		}

		[Route("games/{gameId}")]
		[HttpDelete]
		public async Task<ActionResult> DeleteGameAsync(Guid gameId)
		{
			var command=new DeleteGameCommand(gameId);
			await _deleteGameCommandHandler.HandleAsync(command);
			return Ok();
		}

		[Route( "games/{gameId}/goal-stats" )]
		[HttpGet]
		public async Task<ActionResult<ICollection<GoalStatsResponse>>> GetGamesGoalStatsAsync( Guid gameId )
		{
			var query=new GetGamesGoalStatsQuery(gameId);
			var stats = await _getGamesGoalStatsQueryHandler.HandleAsync(query);
			return Ok( stats );
		}
	}
}