using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Threading.Tasks;

using HotChanApi.Data;
using HotChanApi.Models;

using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace HotChanApi.Data
{
	public  class ThreadBox : IThreadBox
	{
		private readonly DataContext _db;
		// This value has to be stored and updated in a db or a  file.
		 //long gets = 0;
		public ThreadBox(DataContext db)
		{
			_db = db;
		}
		public  async Task<Post> NewPost(Post post)
		{
			// thread related metadata
			//InitPost(ref post);
			//post.isHeadThread = true;
			// Now Push 'thread' to the DB
			post.time = DateTime.Now;
			await _db.Posts.AddAsync(post);
			await _db.SaveChangesAsync().ConfigureAwait(false);

			return post;
		}

		public async Task<Post> NewPost(PostDialogueDto newPostDialogueDto)
		{
			// check if content type is 'image' or 'video'
			if (newPostDialogueDto.file.ContentType.Contains("image") ||
				newPostDialogueDto.file.ContentType.Contains("video"))
			{
				using (var ms = new MemoryStream())
				{
					await newPostDialogueDto.file.CopyToAsync(ms);
					var fileBytes = ms.ToArray();
					//string s = Convert.ToBase64String(fileBytes);

					File.WriteAllBytes("~/mediacontent", fileBytes);
				}
			}
			else
			{
				return null;
			}
			var newPost = new Post
			{
				name = newPostDialogueDto.name,
				title = newPostDialogueDto.title,
				tags = newPostDialogueDto.tags,
				comment = newPostDialogueDto.comment,
				//MediaUrl	= newPostDialogueDto.MediaUrl

			};
			return newPost;
		}

			public  async Task<Reply> ReplyPost(long parentPostId, Reply reply)
		{
			reply.time = DateTime.Now;
			reply.parentPostId = parentPostId;
			
			await _db.Replies.AddAsync(reply);
			var mainPost = await _db.Posts.FirstOrDefaultAsync(x => x.id == parentPostId).ConfigureAwait(false);
			await _db.SaveChangesAsync().ConfigureAwait(false);
			return reply;
		}

		public  async void Prune(long postId)
		{

			var post = _db.Posts.SingleOrDefault(b => b.id == postId);
			if (post != null)
			{
				_db.Posts.Attach(post);
				_db.Posts.Remove(post);
				await _db.SaveChangesAsync().ConfigureAwait(false); ;
			}
			
		}
		
	}
}
