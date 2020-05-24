using System.Collections.Generic;
using System.Linq;
using Sport_A_Lyzer.Models;
using Sport_A_Lyzer.Services.Models;

namespace Sport_A_Lyzer.Extensions
{
	public static class UserExtensions
	{
		public static IEnumerable<User> WithoutPasswords( this IEnumerable<User> users )
		{
			return users.Select( x => x.WithoutPassword() );
		}

		public static User WithoutPassword( this User user )
		{
			user.Password = null;
			return user;
		}

		public static UserResponse ConvertToUserResponse(this User user)
		{
			return new UserResponse()
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserName = user.UserName,
				Organization = user.Organization.Name,
				OrganizationOptions = user.Organization.Options,
				RoleId = user.RoleId,
				Token = user.Token
			};
		}
	}
}
