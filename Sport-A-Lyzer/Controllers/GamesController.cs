using Microsoft.AspNetCore.Mvc;
using Sport_A_Lyzer.CQRS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sport_A_Lyzer.CQRS.GameOperations;
using Sport_A_Lyzer.CQRS.GoalOperations;

namespace Sport_A_Lyzer.Controllers
{
	[Authorize]
	[ApiController]
	public class GamesController : ControllerBase
	{
		private readonly ICommandHandler<UpsertGameCommand> _upsertGameCommandHandler;
		private readonly ICommandHandler<DeleteGameCommand> _deleteGameCommandHandler;
		private readonly IQueryHandler<GetGameQuery, GameResponse> _getGameQueryHandler;
		private readonly IQueryHandler<GetGamesGoalStatsQuery, ICollection<GoalStatsResponse>> _getGamesGoalStatsQueryHandler;
		private readonly ICommandHandler<UpsertGoalCommand> _upsertGoalCommandHandler;
		private readonly ICommandHandler<PauseGameCommand> _pauseGameCommandHandler;
		private readonly ICommandHandler<UnPauseGameCommand> _unPauseGameCommandHandler;

		public GamesController(
			ICommandHandler<UpsertGameCommand> upsertGameCommandHandler,
			ICommandHandler<DeleteGameCommand> deleteGameCommandHandler,
			IQueryHandler<GetGameQuery, GameResponse> getGameQueryHandler,
			IQueryHandler<GetGamesGoalStatsQuery, ICollection<GoalStatsResponse>> getGamesGoalStatsQueryHandler,
			ICommandHandler<UpsertGoalCommand> upsertGoalCommandHandler,
			ICommandHandler<PauseGameCommand> pauseGameCommandHandler,
			ICommandHandler<UnPauseGameCommand> unPauseGameCommandHandler
			)
		{
			_upsertGameCommandHandler = upsertGameCommandHandler;
			_deleteGameCommandHandler = deleteGameCommandHandler;
			_getGameQueryHandler = getGameQueryHandler;
			_getGamesGoalStatsQueryHandler = getGamesGoalStatsQueryHandler;
			_upsertGoalCommandHandler = upsertGoalCommandHandler;
			_pauseGameCommandHandler = pauseGameCommandHandler;
			_unPauseGameCommandHandler = unPauseGameCommandHandler;
		}

		[Route( "api/games/{gameId}" )]
		[HttpGet]
		public async Task<ActionResult<GameResponse>> GetGameAsync( Guid gameId )
		{
			var query = new GetGameQuery( gameId );
			var game = await _getGameQueryHandler.HandleAsync( query );
			return Ok( game );
		}

		[Route( "api/games/{gameId}" )]
		[HttpPost]
		public async Task<ActionResult> PostUpsertGameAsync( Guid gameId, [FromBody]UpsertGameRequest request )
		{
			var command = new UpsertGameCommand( gameId, request );
			await _upsertGameCommandHandler.HandleAsync( command );
			return Ok();
		}

		[Route( "api/games/{gameId}" )]
		[HttpDelete]
		public async Task<ActionResult> DeleteGameAsync( Guid gameId )
		{
			var command = new DeleteGameCommand( gameId );
			await _deleteGameCommandHandler.HandleAsync( command );
			return Ok();
		}

		[Route( "api/games/{gameId}/goal-stats" )]
		[HttpGet]
		public async Task<ActionResult<ICollection<GoalStatsResponse>>> GetGamesGoalStatsAsync( Guid gameId )
		{
			var query = new GetGamesGoalStatsQuery( gameId );
			var stats = await _getGamesGoalStatsQueryHandler.HandleAsync( query );
			return Ok( stats );
		}

		[Route( "api/goal/{goalId}" )]
		[HttpPost]
		public async Task<ActionResult> PostGoalAsync( Guid goalId, [FromBody]UpsertGoalRequest request )
		{
			var command = new UpsertGoalCommand( goalId, request );
			await _upsertGoalCommandHandler.HandleAsync( command );
			return Ok();
		}

		[Route( "api/games/{gameId}/pause" )]
		[HttpPost]
		public async Task<ActionResult> PostPauseGameAsync( Guid gameId, InsertClockEventRequest request )
		{
			var command = new PauseGameCommand( gameId, request.EventTimeStamp, request.GameStartTime );
			await _pauseGameCommandHandler.HandleAsync( command );
			return Ok();
		}

		[Route( "api/games/{gameId}/unpause" )]
		[HttpPost]
		public async Task<ActionResult> PostUnPauseGameAsync( Guid gameId, InsertClockEventRequest request )
		{
			var command = new UnPauseGameCommand( gameId, request.EventTimeStamp, request.GameStartTime );
			await _unPauseGameCommandHandler.HandleAsync( command );
			return Ok();
		}
	}
}