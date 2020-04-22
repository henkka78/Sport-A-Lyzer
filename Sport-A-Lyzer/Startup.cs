using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sport_A_Lyzer.Helpers;
using Sport_A_Lyzer.Models;
using Sport_A_Lyzer.Services;


namespace Sport_A_Lyzer
{
	public class Startup
	{
		private readonly string CorsPolicyName = "MyCorsPolicy";

		public Startup( IConfiguration configuration )
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices( IServiceCollection services )
		{
			services.AddCors( options =>
			{
				options.AddPolicy( name: CorsPolicyName,
					builder =>
					{
						builder.WithOrigins("http://bloodyhanks.com", "http://www.bloodyhanks.com")
							.AllowAnyMethod()
							.AllowAnyHeader();
					} );
			} );
			services.AddControllersWithViews();

			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles( configuration =>
			{
				configuration.RootPath = "ClientApp/dist/ng-uikit-pro-standard";
			} );
			services.AddControllers();
			

			var appSettingsSection = Configuration.GetSection( "AppSettings" );
			services.Configure<AppSettings>( appSettingsSection );

			// configure jwt authentication
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes( appSettings.Secret );
			services.AddAuthentication( x =>
				{
					x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				} )
				.AddJwtBearer( x =>
				{
					x.RequireHttpsMetadata = false;
					x.SaveToken = true;
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey( key ),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				} );



			services.AddDbContext<SportALyzerAppDbContext>( options =>
				options.UseSqlServer( Configuration.GetConnectionString( "AppDatabase" ) ) );
			services.AddScoped<IUserService, UserService>();

			DependencyInjection.Bootstrap.RegisterInterfaceImplementations( services );
			services.AddSwaggerGen( c =>
			{
				c.SwaggerDoc( "v1", new OpenApiInfo { Title = "SportALyzer_API", Version = "v1" } );
			} );
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
		{
			app.UseCors( CorsPolicyName );
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
			if ( !env.IsDevelopment() )
			{
				app.UseSpaStaticFiles();
			}

			app.UseRouting();
			
			app.UseAuthentication();
			app.UseAuthorization();
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
					 spa.UseAngularCliServer( npmScript: "start" );
				 }
			 } );
		}
	}
}
