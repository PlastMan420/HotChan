using GreenDonut;
using HotChan.DataBase;
using HotChan.DataBase.Models;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotChan.DataAccess.DataLoader
{
	public class UsersDL : BatchDataLoader<Guid, User>
	{
        private readonly IDbContextFactory<HotChanContext> _dbContextFactory;

        public UsersDL(
            IBatchScheduler batchScheduler,
            IDbContextFactory<HotChanContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));
        }


        protected override async Task<IReadOnlyDictionary<Guid, User>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken
            )
        {
            await using HotChanContext dbContext = _dbContextFactory.CreateDbContext();

            return await dbContext.Users
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}
