using GreenDonut;
using HotChan.DataBase;
using HotChan.DataBase.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotChan.DataAccess.DataLoader
{
    public class PostsBatchDL : BatchDataLoader<Guid, Post>
    {
        private readonly IDbContextFactory<HotChanContext> _dbContextFactory;

        public PostsBatchDL(
            IBatchScheduler batchScheduler,
            IDbContextFactory<HotChanContext> dbContextFactory,
            DataLoaderOptions options
            ) : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<Guid, Post>> LoadBatchAsync(
            IReadOnlyList<Guid> keys, 
            CancellationToken cancellationToken
            )
        {
            await using HotChanContext dbContext =
                _dbContextFactory.CreateDbContext();

            return await dbContext.Posts
                .Where(s => keys.Contains(s.PostId))
                .Include(u => u.User)
                .ToDictionaryAsync(t => t.PostId, cancellationToken);
        }
    }
}
