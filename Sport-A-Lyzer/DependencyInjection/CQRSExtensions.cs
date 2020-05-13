using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Sport_A_Lyzer.DependencyInjection
{
	public static class CqrsExtensions
	{
		public static void AddCqrsHandlers( this IServiceCollection services, Type handlerInterface )
		{
			var handlers = typeof( Startup ).Assembly.GetTypes()
				.Where( t => t
					.GetInterfaces()
					.Any( i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface ) );

			foreach ( var handler in handlers )
			{
				services.AddScoped( handler.GetInterfaces().First( i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface ), handler );
			}
		}
	}
}
