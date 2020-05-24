using System.Collections.Generic;
using System.Threading.Tasks;
using Sport_A_Lyzer.Models;
using Sport_A_Lyzer.Services.Models;

namespace Sport_A_Lyzer.Services
{
	public interface IUserService
	{
		Task<UserResponse> Authenticate( string username, string password );
		IEnumerable<User> GetAll();
		Task AddUser( UserRequest userRequest );
	}
}