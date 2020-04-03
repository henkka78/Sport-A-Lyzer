using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.Models
{
	public partial class User
	{
		[NotMapped]
		public string Token { get; set; }
	}
}
