using System.Threading.Tasks;
using HotChan.DataBase.Models;

namespace HotChan.DataAccess.Repository
{
	public interface IUserRepository
	{
		public Task<User> CreateUser(User newUser, string password);
	}
}
