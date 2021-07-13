using HotChan.DataBase;
using HotChan.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolate.Data;
using HotChocolate;

namespace HotChan.DataAccess.Data
{
	public class PostRepo
	{

		[UseDbContext(typeof(HotChanContext))]
		public Post GetPost([ScopedService] HotChanContext hotchanContext, Guid PostId)
		{
			return hotchanContext.Posts.FirstOrDefault(x => x.PostId == PostId);
		}

		[UseDbContext(typeof(HotChanContext))]
		public IQueryable<Post> PostCatalog([ScopedService] HotChanContext hotchanContext)
			=> hotchanContext.Posts;
		
	}
}
