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
			await _db.Posts.AddAsync(post);
			await _db.SaveChangesAsync().ConfigureAwait(false);

			return post;
		}


			public  async Task<Reply> ReplyPost(long postId, Reply reply)
		{
			reply.Time = DateTime.Now;
			reply.PostId = postId;
			
			await _db.Replies.AddAsync(reply);
			var mainPost = await _db.Posts.FirstOrDefaultAsync(x => x.PostId == postId).ConfigureAwait(false);
			await _db.SaveChangesAsync().ConfigureAwait(false);
			return reply;
		}

		public  async void Prune(long postId)
		{
			// TODO: remove replies from the replies table
			var post = _db.Posts.SingleOrDefault(b => b.PostId == postId);
			if (post != null)
			{
				_db.Posts.Attach(post);
				_db.Posts.Remove(post);
				await _db.SaveChangesAsync().ConfigureAwait(false); ;
			}
			
		}
		
	}
}
