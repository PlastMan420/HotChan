using HotChan.DataAccess.DataLoader;
using HotChan.DataAccess.Filters;
using HotChan.DataBase;
using HotChan.DataBase.Extensions;
using HotChan.DataBase.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotChan.DataAccess.Users
{
	[ExtendObjectType(Name = "Query")]
	public class UserQuery
	{
		public IQueryable<User> GetUsers(
			[ScopedService] HotChanContext hotchanContext) =>
			hotchanContext.Users;

		[UseApplicationDbContext]
		public Task<User> GetUserAsync(
			UsersDL userIdDl,
			CancellationToken cancellationToken,
			[GraphQLName("UserId")] Guid userId
			)
			=> userIdDl.LoadAsync(userId, cancellationToken);

		[UseApplicationDbContext]
		public async Task<List<User>> GetUsersAsync([ScopedService] HotChanContext hotchanContext)
			=> await hotchanContext.Users.ToListAsync();
	}
}
