using HotChan.DataAccess.DataLoader;
using HotChan.DataAccess.Filters;
using HotChan.DataBase;
using HotChan.DataBase.Extensions;
using HotChan.DataBase.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
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
		[UseApplicationDbContext]
		[UsePaging]
		public IQueryable<User> GetUsers(
			[ScopedService] HotChanContext hotchanContext) 
			=>	hotchanContext.Users;

		public Task<User> GetUserAsync(
			UsersDL UserIdDl,
			CancellationToken cancellationToken,
			[GraphQLName("UserId")] Guid UserId)
			=>	UserIdDl.LoadAsync(UserId, cancellationToken);

		public async Task<IEnumerable<User>> GetUsersByIdAsync(
			   [ID(nameof(User))] Guid[] ids,
			   UsersDL UserIdDl,
			   CancellationToken cancellationToken) 
			=>	await UserIdDl.LoadAsync(ids, cancellationToken);
	}
}
