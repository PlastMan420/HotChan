using GreenDonut;
using HotChan.DataBase;
using HotChan.DataBase.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotChan.DataAccess.DataLoader
{
	public class UserSubmissionsDL : GroupedDataLoader<Guid, Post>
	{
        private readonly IDbContextFactory<HotChanContext> _dbContextFactory;

        public UserSubmissionsDL(
        IBatchScheduler batchScheduler,
		IDbContextFactory<HotChanContext> dbContextFactory)
		: base(batchScheduler)
		{
			_dbContextFactory = dbContextFactory ??
			throw new ArgumentNullException(nameof(dbContextFactory));
		}

		protected override async Task<ILookup<Guid, Post>> LoadGroupedBatchAsync(
			IReadOnlyList<Guid> key,
			CancellationToken cancellationToken)
		{
			await using HotChanContext dbContext = _dbContextFactory.CreateDbContext();

			var posts =  dbContext.Posts.Where(x => x.Id == key.ElementAt(0));
			return posts.ToLookup(x => x.Id);

		}
	}
}
