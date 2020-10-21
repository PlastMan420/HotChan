﻿using System;
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

		public  async Task<Reply> ReplyPost(long headGet, Reply reply)
		{
			//InitPost(ref reply);
			//post.isHeadThread = false;

			// get post by id (Get number)
			reply.time = DateTime.Now;
			reply.parentPostGet = headGet;
			
			await _db.Replies.AddAsync(reply);
			var mainPost = await _db.Posts.FirstOrDefaultAsync(x => x.get == headGet).ConfigureAwait(false);
			await _db.SaveChangesAsync().ConfigureAwait(false);
			return reply;
		}

		public  async void Prune(long getId)
		{

			var post = _db.Posts.SingleOrDefault(b => b.get == getId);
			if (post != null)
			{
				_db.Posts.Attach(post);
				_db.Posts.Remove(post);
				await _db.SaveChangesAsync().ConfigureAwait(false); ;
			}
			
		}
		public  async void Archiver(long getId)
		{
			// posts become archived if their priority falls and are caught by the garbage collector when it runs
			// Reply posts are marked as archived as well as the head post

			var post = _db.Posts.SingleOrDefault(b => b.get == getId);
			if (post != null)
			{
				post.isArchived = true;
				await _db.SaveChangesAsync().ConfigureAwait(false);
			}
			
		}

		// Convert this thing into a service --->

		// The garbage collector removes archived posts after 24h of archiving.

		//	public  async void GarbageCollector()
		//	{
		//		// if thread archived or pruned and lifetime > 1 day: remove from db
		//		// sleep for 12h
		//		string sqlDeleteStatement = "DELETE FROM Posts WHERE isArchived = 1";
		//		using (var db = new DataContext())
		//		{
		//			// context.Customers.Where(x => !x.IsActive).DeleteFromQuery();
		//			//db.Posts.Where(x => x.isArchived).DeleteFromQuery();
		//			var toRemovePosts = db.Posts
		//				.Where(m => m.isArchived)
		//				.ToList();
		//			var q = from c in db.Posts
		//					where c.isArchived == true
		//					select c;
		//			q.Delete();

		//		}
		//	}
		
	}
}