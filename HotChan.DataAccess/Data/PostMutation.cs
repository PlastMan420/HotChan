using HotChan.DataBase;
using HotChan.DataBase.Extensions;
using HotChan.DataBase.Models;
using HotChocolate;
using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChan.DataAccess.Data
{
	public class PostMutation
	{
		[UseApplicationDbContext]
		public async Task<Guid> AddPost([ScopedService] HotChanContext hotchanContext, PostDialogueDto req)
		{
			var post = new Post
			{
				PostTitle = req.PostTitle,
				Description = req.Description,
				Time = DateTimeOffset.Now,
				Id = req.UserId,
			};

			hotchanContext.Posts.Add(post);
			await hotchanContext.SaveChangesAsync();
			return post.PostId;
		}
	}
}
