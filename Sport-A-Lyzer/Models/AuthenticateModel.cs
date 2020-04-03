using System.ComponentModel.DataAnnotations;

namespace Sport_A_Lyzer.Models
{
	public class AuthenticateModel
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
