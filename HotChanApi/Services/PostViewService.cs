using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Grpc.Core;
using HotChanApi.Data;
using HotChanShared.Models;
using Microsoft.EntityFrameworkCore;

namespace HotChanApi.Services
{
	public class PostViewService : Chan.ChanBase
	{
		private readonly DataContext _db;

		public PostViewService(DataContext context)
		{
			context = this._db;
		}

		// All types are from HotChanShared.Models and not to be confused with types in HotChanApi
		public override Task<PostReply> PostQuery(PostIdRequest request, ServerCallContext context)
		{
			var reply = new PostReply { };
			var post = _db.Posts.FirstOrDefaultAsync(x => x.PostId.ToString() == request.PostId.ToString());
			
			return Task.FromResult(reply);
		}
	}
}
