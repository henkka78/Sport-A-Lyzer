using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.CQRS.GameEventOperations;
using Sport_A_Lyzer.CQRS.GameOperations;
using Sport_A_Lyzer.CQRS.GoalOperations;
using Sport_A_Lyzer.CQRS.PlayerOperations;
using Sport_A_Lyzer.CQRS.TeamOperations;
using Sport_A_Lyzer.CQRS.TournamentOperations;

namespace Sport_A_Lyzer.DependencyInjection
{
	public class Bootstrap
	{
		public static void RegisterInterfaceImplementations( IServiceCollection services )
		{
			services
				.AddScoped<IQueryHandler<GetGamesByTournamentIdQuery, ICollection<GameResponse>>,
					GetGamesByTournamentIdQueryHandler>()
				.AddScoped<IQueryHandler<GetTournamentsByYearQuery, ICollection<TournamentResponse>>,
					GetTournamentsByYearQueryHandler>()
				.AddScoped<IQueryHandler<GetGameQuery, GameResponse>, GetGameQueryHandler>()
				.AddScoped<ICommandHandler<UpsertGameCommand>, UpsertGameCommandHandler>()
				.AddScoped<ICommandHandler<DeleteGameCommand>, DeleteGameCommandHandler>()
				.AddScoped<ICommandHandler<UpsertTeamCommand>, UpsertTeamCommandHandler>()
				.AddScoped<ICommandHandler<UpsertPlayerCommand>, UpsertPlayerCommandHandler>()
				.AddScoped<ICommandHandler<UpsertGameEventCommand>, UpsertGameEventCommandHandler>()
				.AddScoped<IQueryHandler<GetGamesGoalStatsQuery, ICollection<GoalStatsResponse>>,
					GetGamesGoalStatsQueryHandler>()
				.AddScoped<IQueryHandler<GetTeamsPlayersQuery, ICollection<PlayerResponse>>, GetTeamsPlayersQueryHandler
				>()
				.AddScoped<ICommandHandler<UpsertGoalCommand>, UpsertGoalCommandHandler>()
				.AddScoped<IQueryHandler<GetTeamsQuery, ICollection<TeamResponse>>, GetTeamsQueryHandler>()
				.AddScoped<ICommandHandler<PauseGameCommand>, PauseGameCommandHandler>()
				.AddScoped<ICommandHandler<UnPauseGameCommand>, UnPauseGameCommandHandler>();
		}
	}
}
