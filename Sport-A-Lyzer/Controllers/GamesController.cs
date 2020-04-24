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
		private readonly IQueryHandler<GetGameQuery, GameFollowResponse> _getGameQueryHandler;
		private readonly IQueryHandler<GetGamesGoalStatsQuery, ICollection<GoalStatsResponse>> _getGamesGoalStatsQueryHandler;
		private readonly ICommandHandler<UpsertGoalCommand> _upsertGoalCommandHandler;
		private readonly ICommandHandler<SetGamePauseStatusCommand> _setGamePauseStatusCommandHandler;
		private readonly IQueryHandler<GetGamesByTournamentIdQuery, ICollection<GameResponse>> _getGamesByTournamentIdQueryHandler;
		private readonly ICommandHandler<StartGameCommand> _startGameCommandHandler;
		private readonly ICommandHandler<EndGameCommand> _endGameCommandHandler;

		public GamesController(
			ICommandHandler<UpsertGameCommand> upsertGameCommandHandler,
			ICommandHandler<DeleteGameCommand> deleteGameCommandHandler,
			IQueryHandler<GetGameQuery, GameFollowResponse> getGameQueryHandler,
			IQueryHandler<GetGamesGoalStatsQuery, ICollection<GoalStatsResponse>> getGamesGoalStatsQueryHandler,
			ICommandHandler<UpsertGoalCommand> upsertGoalCommandHandler,
			IQueryHandler<GetGamesByTournamentIdQuery, ICollection<GameResponse>> getGamesByTournamentIdQueryHandler,
			ICommandHandler<StartGameCommand> startGameCommandHandler,
			ICommandHandler<EndGameCommand> endGameCommandHandler,
			ICommandHandler<SetGamePauseStatusCommand> setGamePauseStatusCommandHandler
			)
		{
			_upsertGameCommandHandler = upsertGameCommandHandler;
			_deleteGameCommandHandler = deleteGameCommandHandler;
			_getGameQueryHandler = getGameQueryHandler;
			_getGamesGoalStatsQueryHandler = getGamesGoalStatsQueryHandler;
			_upsertGoalCommandHandler = upsertGoalCommandHandler;
			_getGamesByTournamentIdQueryHandler = getGamesByTournamentIdQueryHandler;
			_startGameCommandHandler = startGameCommandHandler;
			_endGameCommandHandler = endGameCommandHandler;
			_setGamePauseStatusCommandHandler = setGamePauseStatusCommandHandler;
		}

		[Route( "api/games/{gameId}" )]
		[HttpGet]
		public async Task<ActionResult<GameFollowResponse>> GetGameAsync( Guid gameId )
		{
			var query = new GetGameQuery( gameId );
			var game = await _getGameQueryHandler.HandleAsync( query );
			return Ok( game );
		}

		[Route( "api/games" )]
		[HttpGet]
		public async Task<ActionResult<ICollection<GameResponse>>> GetGamesAsync()
		{
			var query = new GetGamesByTournamentIdQuery( null );
			var games = await _getGamesByTournamentIdQueryHandler.HandleAsync( query );
			return Ok( games );
		}

		[Route( "api/games/{gameId}" )]
		[HttpPut]
		public async Task<ActionResult> PutUpsertGameAsync( Guid gameId, [FromBody]UpsertGameRequest request )
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
		[HttpPut]
		public async Task<ActionResult<GameFollowResponse>> PutGoalAsync( Guid goalId, UpsertGoalRequest request )
		{
			var command = new UpsertGoalCommand( goalId, request );
			await _upsertGoalCommandHandler.HandleAsync( command );

			var query=new GetGameQuery(request.GameId);
			var game = await _getGameQueryHandler.HandleAsync(query);

			return Ok(game);
		}

		[Route( "api/games/{gameId}/set-pause-status" )]
		[HttpPost]
		public async Task<ActionResult> PostPauseStatusAsync( Guid gameId, SetPauseStatusRequest request )
		{
			var command = new SetGamePauseStatusCommand( gameId, request.EventTimeStamp, request.IsActivePause );
			await _setGamePauseStatusCommandHandler.HandleAsync( command );
			return Ok();
		}

		[Route( "api/games/{gameId}/start" )]
		[HttpPost]
		public async Task<ActionResult> PostStartGameAsync( Guid gameId, StartGameRequest request )
		{
			var command = new StartGameCommand(gameId, request.StartTime);
			await _startGameCommandHandler.HandleAsync(command);
			return Ok();
		}

		[Route( "api/games/{gameId}/end" )]
		[HttpPost]
		public async Task<ActionResult> PostEndGameAsync( Guid gameId, EndGameRequest request )
		{
			var command = new EndGameCommand( gameId, request.EndTime );
			await _endGameCommandHandler.HandleAsync( command );
			return Ok();
		}

	}
}