using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HotChanApi.Models;

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
            var postListJson = System.IO.File.ReadAllText("Data/MOCK_DATA.json");
            var posts = JsonSerializer.Deserialize<List<Post>>(postListJson);
            foreach (var post in posts)
            {
                _context.Posts.Add(post);
            }

            _context.SaveChanges();
        }
    }
}
