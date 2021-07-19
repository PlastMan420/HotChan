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
		public async Task<Post> AddPost([ScopedService] HotChanContext hotchanContext,
			Guid userId,
			string postTitle,
			string description,
			string[] tags,
			Uri mediaUrl

			)
		{
			var post = new Post
			{
				PostTitle = postTitle,
				Description = description,
				Time = DateTimeOffset.Now,
				Tags = tags,
				MediaUrl = mediaUrl,
				Id = userId,
				User = hotchanContext.Users.FirstOrDefault(d => d.Id == userId)
			};


			await hotchanContext.Posts.AddAsync(post);

			await hotchanContext.SaveChangesAsync();

			return post;
		}
	}
}
