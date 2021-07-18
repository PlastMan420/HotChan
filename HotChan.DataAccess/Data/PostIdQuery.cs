using HotChan.DataBase;
using HotChan.DataBase.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChan.DataAccess.Data
{
	[ExtendObjectType(Name = "Query")]
	public class PostIdQuery
	{
		[UseDbContext(typeof(HotChanContext))]
		public Post GetPostById([ScopedService] HotChanContext context, [GraphQLName("PostId")] Guid postId)
		=> context.Posts.FirstOrDefault(a => a.PostId == postId);
	}
}
