using HotChanApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace HotChanApi.Data
{
	public class Seed
	{
		private readonly DataContext _context;
		private readonly UserManager<User> _userManager;

		public Seed(DataContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public void SeedUsers()
		{
			
			var postListJson = System.IO.File.ReadAllText("Data/post.json");
			var posts = JsonSerializer.Deserialize<List<Post>>(postListJson);
			foreach (var post in posts)
			{
				_context.Posts.Add(post);
				//_userManager.CreateAsync()
			}

			_context.SaveChanges();
			

		}
	}
}