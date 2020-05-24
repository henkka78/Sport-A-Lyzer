using System.ComponentModel.DataAnnotations.Schema;

namespace Sport_A_Lyzer.Models
{
	public partial class User
	{
		[NotMapped]
		public string Token { get; set; }
	}
}
