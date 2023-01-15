using GreenDonut;
using HotChan.DataBase;
using HotChan.DataBase.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotChan.Api.Schema.Loaders;
public class PostsBatchDL : BatchDataLoader<Guid, Post>
{
    private readonly IDbContextFactory<HotChanContext> _dbContextFactory;

    public PostsBatchDL(
        IDbContextFactory<HotChanContext> dbContextFactory,
        IBatchScheduler batchScheduler,
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
        // await using automatically disposes of dbContext.
        //https://chillicream.com/docs/hotchocolate/v12/integrations/entity-framework#dataloaders
        await using HotChanContext dbContext =
            _dbContextFactory.CreateDbContext();

        return await dbContext.Posts
            .Where(s => keys.Contains(s.PostId))
            .ToDictionaryAsync(t => t.PostId, cancellationToken);
    }
}
