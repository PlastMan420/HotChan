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
using HotChocolate.Types.Relay;

namespace HotChan.DataAccess.Data
{
	[ExtendObjectType(Name = "Query")]
	public class PostQuery
	{
		[UseApplicationDbContext]
		[UsePaging]
		public IQueryable<Post> GetPosts(
			[ScopedService] HotChanContext hotchanContext) =>
			hotchanContext.Posts
			.Include(u => u.User);

		public Task<Post> GetPostAsync(
			PostsDL postIdDl,
			CancellationToken cancellationToken,
			[GraphQLName("PostId")] Guid PostId
			)
			=> postIdDl.LoadAsync(PostId, cancellationToken);

		public async Task<IEnumerable<Post>> GetPostsByIdAsync(
			   [ID(nameof(Post))] Guid[] ids,
			   PostsDL postIdDl,
			   CancellationToken cancellationToken) =>
			   await postIdDl.LoadAsync(ids, cancellationToken);
	}
}

// https://github.com/ChilliCream/graphql-workshop/blob/master/docs/3-understanding-dataLoader.md
