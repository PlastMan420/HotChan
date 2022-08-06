using HotChan.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Types;
using HotChan.DataBase.Extensions;
using HotChan.DataAccess.Gql.DataLoader;
using System.Threading;
using HotChocolate.Types.Relay;
using HotChan.DataBase.Models.Entities;

namespace HotChan.DataAccess.Gql.Data
{
	public class PostQuery
	{
		[UseApplicationDbContext]
		[UsePaging]
		public IQueryable<Post> GetPosts(
			[ScopedService] HotChanContext hotchanContext) =>
			hotchanContext.Posts
			.Include(u => u.User);

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
}

// https://github.com/ChilliCream/graphql-workshop/blob/master/docs/3-understanding-dataLoader.md
