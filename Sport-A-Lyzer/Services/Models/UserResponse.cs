using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.Services.Models
{
	public class UserResponse
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public Guid RoleId { get; set; }
		public string Organization { get; set; }
		public string OrganizationOptions { get; set; }
		public string Token { get; set; }
		public string Password { get; set; }

	}
}
