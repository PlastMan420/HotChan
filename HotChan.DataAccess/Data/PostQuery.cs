using HotChan.DataBase;
using HotChan.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate.Data;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Types;
using HotChan.DataBase.Extensions;
using HotChan.DataAccess.DataLoader;
using System.Threading;

namespace HotChan.DataAccess.Data
{
	[ExtendObjectType(Name = "Query")]
	public class PostQuery
	{
		public IQueryable<Post> GetPosts(
			[ScopedService] HotChanContext hotchanContext) =>
			hotchanContext.Posts;

		[UseApplicationDbContext]
		public Task<Post> GetPostAsync(
			PostsDL postIdDl,
			CancellationToken cancellationToken,
			[GraphQLName("PostId")] Guid PostId
			)
			=> postIdDl.LoadAsync(PostId, cancellationToken);

		[UseApplicationDbContext]
		public async Task<List<Post>> GetPostsAsync(
			[ScopedService] HotChanContext hotchanContext)
			=> await hotchanContext.Posts.ToListAsync();

	}
}

// https://github.com/ChilliCream/graphql-workshop/blob/master/docs/3-understanding-dataLoader.md
