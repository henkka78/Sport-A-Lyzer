using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sport_A_Lyzer.CQRS;
using Sport_A_Lyzer.GameOperations;
using Sport_A_Lyzer.Models;
using Sport_A_Lyzer.TournamentOperations;

namespace Sport_A_Lyzer
{
	public class Startup
	{
		public Startup( IConfiguration configuration )
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices( IServiceCollection services )
		{
			services.AddControllersWithViews();
			services.AddControllers();


			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles( configuration =>
			 {
				 configuration.RootPath = "ClientApp/dist";
			 } );

			services.AddDbContext<SportALyzerAppDbContext>( options =>
				options.UseSqlServer( Configuration.GetConnectionString( "AppDatabase" ) ) );

			DependencyInjection.Bootstrap.RegisterInterfaceImplementations(services);
			services.AddSwaggerGen( c =>
			{
				c.SwaggerDoc( "v1", new OpenApiInfo { Title = "SportALyzer_API", Version = "v1" } );
			} );
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
		{
			if ( env.IsDevelopment() )
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler( "/Error" );
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseSwagger();
			app.UseSwaggerUI( c =>
			{
				c.SwaggerEndpoint( "/swagger/v1/swagger.json", "SportALyzer_API V1" );
			} );

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseRouting();

			app.UseEndpoints( endpoints =>
			 {
				 endpoints.MapControllerRoute(
					 name: "default",
					 pattern: "{controller}/{action=Index}/{id?}" );
			 } );

			app.UseSpa( spa =>
			 {
				 spa.Options.SourcePath = "ClientApp";

				 if ( env.IsDevelopment() )
				 {
					 spa.UseReactDevelopmentServer( npmScript: "start" );
				 }
			 } );
		}
	}
}
