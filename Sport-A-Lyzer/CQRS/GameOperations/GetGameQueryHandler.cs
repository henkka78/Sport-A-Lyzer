using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_A_Lyzer.CQRS.GoalOperations;
using Sport_A_Lyzer.CQRS.PlayerOperations;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.CQRS.GameOperations
{
	internal class GetGameQueryHandler : IQueryHandler<GetGameQuery, GameFollowResponse>
	{
		private readonly SportALyzerAppDbContext _context;
		private readonly IQueryHandler<GetGamesGoalStatsQuery, ICollection<GoalStatsResponse>> _getGoalStatsQueryHandler;

		public GetGameQueryHandler(
			SportALyzerAppDbContext context,
			IQueryHandler<GetGamesGoalStatsQuery, ICollection<GoalStatsResponse>> getGoalStatsQueryHandler )
		{
			_context = context;
			_getGoalStatsQueryHandler = getGoalStatsQueryHandler;
		}
		public async Task<GameFollowResponse> HandleAsync( GetGameQuery query )
		{
			var game = await _context.Game
				.Where( g => g.Id == query.GameId )
				.Include( g => g.AwayTeam )
				.ThenInclude( t => t.Player )
				.Include( g => g.HomeTeam )
				.ThenInclude( t => t.Player )
				.Include( g => g.GamePause )
				.Select( g => new GameFollowResponse()
				{
					Id = g.Id,
					HomeTeamName = g.HomeTeam.Name,
					HomeTeamId = g.HomeTeamId,
					AwayTeamName = g.AwayTeam.Name,
					AwayTeamId = g.AwayTeamId,
					IsPaused = IsPaused( g.GamePause ),
					PausesHeld = g.GamePause.Count,
					SecondsPlayed = GetPlayedSeconds( g.ActualStartTime.Value, g.GamePause ),
					GameDay = g.GameDay,
					StartTime = g.StartTime,
					ActualStartTime = g.ActualStartTime,
					ActualEndTime = g.ActualEndTime,
					Players = GetPlayers(g.HomeTeam, g.AwayTeam)
				} ).SingleOrDefaultAsync();

			if ( game == null )
			{
				throw new InvalidOperationException( "Antamallasi ID:llä ei löydy aktiivista peliä!" );
			}

			if ( game.ActualStartTime != null )
			{
				game.GoalStats = await GetGoalStats( game.Id );
			}

			return game;
		}

		private static Dictionary<string, ICollection<PlayerResponse>> GetPlayers(Team homeTeam, Team awayTeam)
		{
			var result=new Dictionary<string, ICollection<PlayerResponse>>();

			var homeTeamPlayers = homeTeam.Player.Select(p => new PlayerResponse()
			{
				FirstName = p.FirstName,
				LastName = p.LastName,
				PlayerId = p.Id,
				PlayerNumber = p.Number
			}).ToList();

			var awayTeamPlayers=awayTeam.Player.Select( p => new PlayerResponse()
			{
				FirstName = p.FirstName,
				LastName = p.LastName,
				PlayerId = p.Id,
				PlayerNumber = p.Number
			} ).ToList();

			result.Add(homeTeam.Id.ToString(), homeTeamPlayers);
			result.Add(awayTeam.Id.ToString(), awayTeamPlayers);

			return result;
		}

		private async Task<ICollection<GoalStatsResponse>> GetGoalStats( Guid gameId )
		{
			var query = new GetGamesGoalStatsQuery( gameId );
			return await _getGoalStatsQueryHandler.HandleAsync( query );
		}

		private static bool IsPaused( ICollection<GamePause> gamePauses )
		{
			return gamePauses.Any( gp => gp.IsActivePause );
		}

		private static int GetPlayedSeconds( DateTime gameStartTime, ICollection<GamePause> gamePauses )
		{
			var secondsPaused = gamePauses.Where( gp => gp.EndTime != null ).Aggregate( 0, ( total, current ) =>
			   {
				   if ( current.EndTime.HasValue )
				   {
					   return total + ( int )( current.EndTime.Value - current.StartTime ).TotalSeconds;
				   }

				   return total;
			   } );

			var activePause = gamePauses.SingleOrDefault( gp => gp.EndTime == null );
			if ( activePause != null )
			{
				return ( int )( activePause.StartTime - gameStartTime ).TotalSeconds - secondsPaused;
			}

			return ( int )( DateTime.Now - gameStartTime ).TotalSeconds - secondsPaused;
		}
	}
}
