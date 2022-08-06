using System;
using System.Threading.Tasks;
using HotChan.DataBase.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotChan.DataAccess.Gql.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<User> _userManager;

		public UserRepository(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<User> CreateUser(User newUser, string password)
		{
			var salt = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("+", "-").Replace("/", "_");

			await _userManager.CreateAsync(newUser, password + salt);
			await _userManager.AddToRoleAsync(newUser, "Member");
			
			return newUser;
		}

		public async Task ElevateUser(User user)
		{
			await _userManager.AddToRoleAsync(user, "Admin");
		}

		// TODO. Login
		//public async Task<User> SignIn(string username, string password)
		//{
		//	_userManager.lo
		//}
	}
}
