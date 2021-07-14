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
		public Task<Post> GetPost(
			[ScopedService] HotChanContext hotchanContext,
			PostIdDL postIdDl,
			CancellationToken cancellationToken,
			Guid PostId
		)
			=> postIdDl.LoadAsync(PostId, cancellationToken);

		[UseApplicationDbContext]
		[UsePaging(MaxPageSize = 25)]
		public Task<List<Post>> PostCatalog([ScopedService] HotChanContext hotchanContext)
			=>  hotchanContext.Posts.ToListAsync();

	}
}

// https://github.com/ChilliCream/graphql-workshop/blob/master/docs/3-understanding-dataLoader.md
