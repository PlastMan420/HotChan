//using GreenDonut;
//using HotChan.DataBase;
//using HotChan.DataBase.Models.Entities;
//using Microsoft.EntityFrameworkCore;
//using System;

//namespace HotChan.Api.Schema.Loaders;

//public class UserSubmissionsDL : GroupedDataLoader<Guid, Post>
//{
//        private readonly IDbContextFactory<HotChanContext> _dbContextFactory;

//        public UserSubmissionsDL(
//        IBatchScheduler batchScheduler,
//		IDbContextFactory<HotChanContext> dbContextFactory)
//		: base(batchScheduler)
//		{
//			_dbContextFactory = dbContextFactory ??
//			throw new ArgumentNullException(nameof(dbContextFactory));
//		}

//	protected override async Task<ILookup<Guid, Post>> LoadGroupedBatchAsync(
//		IReadOnlyList<Guid> key,
//		CancellationToken cancellationToken)
//	{
//		await using HotChanContext dbContext = _dbContextFactory.CreateDbContext();

//		var posts =  dbContext.Posts.Where(x => x.user.Id == key.ElementAt(0));
//		return posts.ToLookup(x => x.user.Id);

//	}
//}
