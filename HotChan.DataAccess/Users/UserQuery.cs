using HotChan.DataAccess.DataLoader;
using HotChan.DataBase;
using HotChan.DataBase.Extensions;
using HotChan.DataBase.Models.Entities;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotChan.DataAccess.Users
{
	public class UserQuery
	{
		[UseApplicationDbContext]
		[UsePaging]
		public IQueryable<User> GetUsers(
			[ScopedService] HotChanContext hotchanContext) 
			=>	hotchanContext.Users;

		public Task<User> GetUserAsync(
			UsersBatchDL UserIdDl,
			CancellationToken cancellationToken,
			[GraphQLName("UserId")] Guid UserId)
			=>	UserIdDl.LoadAsync(UserId, cancellationToken);

		public async Task<IEnumerable<User>> GetUsersByIdAsync(
			   [ID(nameof(User))] Guid[] ids,
			   UsersBatchDL UserIdDl,
			   CancellationToken cancellationToken) 
			=>	await UserIdDl.LoadAsync(ids, cancellationToken);
	}
}
