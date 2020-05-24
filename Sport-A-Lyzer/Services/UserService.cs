using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sport_A_Lyzer.Extensions;
using Sport_A_Lyzer.Helpers;
using Sport_A_Lyzer.Models;
using Sport_A_Lyzer.Services.Models;

namespace Sport_A_Lyzer.Services
{
	public class UserService : IUserService
	{
		private readonly SportALyzerAppDbContext _context;
		private readonly AppSettings _appSettings;

		public UserService(
			IOptions<AppSettings> appSettings,
			SportALyzerAppDbContext context )
		{
			_context = context;
			_appSettings = appSettings.Value;
		}

		public async Task<UserResponse> Authenticate( string username, string password )
		{
			var user = await GetUser( username );

			if ( user == null )
			{
				return null;
			}

			if ( !VerifyPassword( user, password ) )
			{
				return null;
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes( _appSettings.Secret );
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity( new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Id.ToString())
				} ),
				Expires = DateTime.UtcNow.AddDays( 1 ),
				SigningCredentials = new SigningCredentials( new SymmetricSecurityKey( key ), SecurityAlgorithms.HmacSha256Signature )
			};
			var token = tokenHandler.CreateToken( tokenDescriptor );
			user.Token = tokenHandler.WriteToken( token );

			return user.ConvertToUserResponse();
		}

		private async Task<User> GetUser( string userName )
		{
			return await _context.User
				.Include( u => u.Organization )
				.SingleOrDefaultAsync( u => u.UserName.ToLower() == userName.ToLower() );
		}

		private static bool VerifyPassword( User user, string password )
		{
			var hashBytes = Convert.FromBase64String( user.Password );
			var salt = new byte[ 16 ];
			Array.Copy( hashBytes, 0, salt, 0, 16 );
			var pbkdf2 = new Rfc2898DeriveBytes( password, salt, 10000 );
			var hash = pbkdf2.GetBytes( 20 );

			for ( var i = 0; i < 20; i++ )
			{
				if ( hashBytes[ i + 16 ] != hash[ i ] )
				{
					return false;
				}
			}

			return true;

		}

		public IEnumerable<User> GetAll()
		{
			var users = _context.User.ToList();
			return users.WithoutPasswords();
		}

		public async Task AddUser( UserRequest userRequest )
		{
			var user = await GetUser( userRequest.UserName );
			if ( user != null )
			{
				throw new InvalidOperationException( "Käyttäjinimi on jo olemassa!" );
			}

			var hashedPassword = HashPassword( userRequest.Password );

			user = new User()
			{
				FirstName = userRequest.FirstName,
				LastName = userRequest.LastName,
				UserName = userRequest.UserName,
				Password = hashedPassword,
				RoleId = userRequest.RoleId
			};
			_context.User.Add( user );
			await _context.SaveChangesAsync();
		}

		private string HashPassword( string password )
		{
			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes( salt = new byte[ 16 ] );

			var pbkdf2 = new Rfc2898DeriveBytes( password, salt, 10000 );
			var hash = pbkdf2.GetBytes( 20 );

			var hashBytes = new byte[ 36 ];
			Array.Copy( salt, 0, hashBytes, 0, 16 );
			Array.Copy( hash, 0, hashBytes, 16, 20 );

			return Convert.ToBase64String( hashBytes );
		}
	}
}
