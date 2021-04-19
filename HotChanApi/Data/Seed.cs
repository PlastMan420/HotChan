using HotChanShared.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace HotChanApi.Data
{
	public class Seed
	{
		private readonly DataContext _context;

		public Seed(DataContext context)
		{
			_context = context;
		}

		public void SeedUsers()
		{
			var postListJson = System.IO.File.ReadAllText("Data/post.json");
			var posts = JsonSerializer.Deserialize<List<Post>>(postListJson);
			foreach (var post in posts)
			{
				_context.Posts.Add(post);
			}

			_context.SaveChanges();
		}
	}
}