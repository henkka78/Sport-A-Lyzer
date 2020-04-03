using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.CQRS.GameOperations;
using Sport_A_Lyzer.CQRS.TournamentOperations;

namespace Sport_A_Lyzer.Controllers
{
	[ApiController]
	[Authorize]
	public class TournamentController : ControllerBase
	{
		private readonly IQueryHandler<GetGamesByTournamentIdQuery, ICollection<GameResponse>> _getGamesByTournamentIdQueryHandler;
		private readonly IQueryHandler<GetTournamentsByYearQuery, ICollection<TournamentResponse>> _getTournamentsByYearQueryHandler;

		public TournamentController(
			IQueryHandler<GetGamesByTournamentIdQuery, ICollection<GameResponse>> getGamesByTournamentIdQueryHandler,
			IQueryHandler<GetTournamentsByYearQuery, ICollection<TournamentResponse>> getTournamentsByYearQueryHandler
			)
		{
			_getGamesByTournamentIdQueryHandler = getGamesByTournamentIdQueryHandler;
			_getTournamentsByYearQueryHandler = getTournamentsByYearQueryHandler;
		}

		[Route( "api/tournaments/{tournamentId}/games" )]
		[HttpGet]
		public async Task<ActionResult<ICollection<GameResponse>>> GetTournamentGamesAsync( Guid tournamentId )
		{
			var query = new GetGamesByTournamentIdQuery( tournamentId );
			var games = await _getGamesByTournamentIdQueryHandler.HandleAsync( query );
			return Ok( games );
		}

		[Route( "api/years/{year}/tournaments" )]
		[HttpGet]
		public async Task<ActionResult<ICollection<TournamentResponse>>> GetTournamentsByYearAsync( int year )
		{
			var query = new GetTournamentsByYearQuery( year );
			var tournaments = await _getTournamentsByYearQueryHandler.HandleAsync( query );
			return Ok( tournaments );
		}
	}
}