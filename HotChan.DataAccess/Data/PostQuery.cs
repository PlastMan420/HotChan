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
	public class PostQuery : IPostQuery
	{

		[UseApplicationDbContext]
		public Task<List<Post>> GetPosts(
			[ScopedService] HotChanContext hotchanContext,
			PostIdDL postIdDl,
			CancellationToken cancellationToken
		)
			=> hotchanContext.Posts.ToListAsync();

		[UseApplicationDbContext]
		[UsePaging(MaxPageSize = 25)]
		public Task<Post> GetPostAsync(
			[ScopedService] HotChanContext hotchanContext,
			PostIdDL postIdDl,
			CancellationToken cancellationToken,
			Guid PostId
			)
			=> postIdDl.LoadAsync(PostId, cancellationToken);

	}
}

// https://github.com/ChilliCream/graphql-workshop/blob/master/docs/3-understanding-dataLoader.md
