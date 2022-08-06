using System.Threading.Tasks;
using HotChan.DataBase.Models.Entities;

namespace HotChan.DataAccess.Gql.Repository
{
	public interface IUserRepository
	{
		public Task<User> CreateUser(User newUser, string password);
	}
}
