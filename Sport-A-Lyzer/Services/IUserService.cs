using System.Collections.Generic;
using System.Threading.Tasks;
using Sport_A_Lyzer.Models;

namespace Sport_A_Lyzer.Services
{
	public interface IUserService
	{
		Task<User> Authenticate( string username, string password );
		IEnumerable<User> GetAll();
		Task AddUser( UserRequest userRequest );
	}
}