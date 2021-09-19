using HotChan.DataAccess.Repository;
using HotChan.DataBase;
using HotChan.DataBase.Extensions;
using HotChan.DataBase.Models;
using HotChocolate;
using System;
using System.Threading.Tasks;

namespace HotChan.DataAccess.Users
{
	class CreateUserMutation
	{
		private readonly IUserRepository _userRepo;
		public CreateUserMutation(IUserRepository userRepo)
		{
			_userRepo = userRepo;
		}

		[UseApplicationDbContext]
		public async Task<UserAddedPayload> RegisterUser(
			[ScopedService] HotChanContext hotchanContext,
			string userName,
			string email,
			string password
		)
		{
			var user = new User
			{
				UserName = userName,
				NormalizedUserName = userName.ToUpper(),
				Email = email,
				NormalizedEmail = email.ToUpper(),
				RegisterationDate = DateTimeOffset.Now,
			};

			await _userRepo.CreateUser(user, password);

			return new UserAddedPayload { CreationTimeOffset = user.RegisterationDate, UserName = user.UserName };
		}
	}
}
