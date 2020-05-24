using System;

namespace Sport_A_Lyzer.Services.Models
{
	public class UserRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public Guid RoleId { get; set; }
	}
}
