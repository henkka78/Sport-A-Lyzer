using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Sport_A_Lyzer.DependencyInjection
{
	public static class CqrsExtensions
	{
		//TODO: Tähän on haussa tyylikkäämin/oikeampi tapa (AutoFac?), jota en ole vielä saanut toimimaan .NET Core 3.1 -ympäristössä. Tämä kuitenkin toimii ja hoitaa homman toistaiseksi
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
