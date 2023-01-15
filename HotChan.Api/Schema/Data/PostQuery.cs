using HotChan.DataBase;
using HotChan.DataBase.Models.Entities;
using HotChan.Api.Schema.Loaders;

namespace HotChan.Api.Schema.Data;

public class PostQuery
{
	[UsePaging]
	public IQueryable<Post> GetPosts(
		[Service(ServiceKind.Synchronized)] HotChanContext hotchanContext) =>
		hotchanContext.Posts;

	public Task<Post> GetPostAsync(
		[Service] PostsBatchDL postIdDl,
		[GraphQLName("PostId")] Guid PostId
		)
		=> postIdDl.LoadAsync(PostId);

	public async Task<IEnumerable<Post>> GetPostsByIdAsync(
		   [ID(nameof(Post))] Guid[] ids,
		   PostsBatchDL postIdDl)
		=> await postIdDl.LoadAsync(ids);
}

// https://github.com/ChilliCream/graphql-workshop/blob/master/docs/3-understanding-dataLoader.md
