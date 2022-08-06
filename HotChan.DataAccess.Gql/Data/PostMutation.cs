using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotChan.DataBase;
using HotChan.DataBase.Extensions;
using HotChan.DataBase.Models.Entities;
using HotChocolate;
using Microsoft.AspNetCore.Http;


namespace HotChan.DataAccess.Gql.Data
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
				UserId = userId,
				User = hotchanContext.Users.FirstOrDefault(d => d.Id == userId)
			};


			await hotchanContext.Posts.AddAsync(post);

			await hotchanContext.SaveChangesAsync();

			return post;
		}

		public async Task<bool> UploadFileAsync(IFormFile file)
		{
			//string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
			string postUploads = @"d:\hcmedia\" + file.FileName;
			DirectoryInfo di = Directory.CreateDirectory(postUploads);
			
			string filePath = System.IO.Path.Combine(postUploads, file.FileName);

			try
			{
				using (Stream fileStream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}
			}
			catch(Exception e)
			{
				Console.WriteLine("Saving file failed: /n" + e);
				return false;
			}

			return true;
		}
	}
}
