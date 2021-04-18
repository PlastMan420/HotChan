using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using HotChanApi.Data;
using HotChanShared.Models;

using AutoMapper;
using Grpc.Core;
using System.Diagnostics;

namespace HotChanApi.Services
{
	public class PostViewService : ChanPostView.ChanPostViewBase
	{
		private readonly DataContext _db;
		private readonly IMapper _mapper;


		public PostViewService(DataContext context, IMapper mapper)
		{
			_db = context;
			_mapper = mapper;

		}

		// All types are from HotChanShared.Models and not to be confused with types in HotChanApi
		public override Task<PostReply> PostQuery(PostIdRequest request, ServerCallContext context)
		{
			//var post = new Post();
			var reply = new PostReply { 
				PostId = 26,
				UserId = 26,
				PostTitle = "hello",
				PostTime=Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow),
				PostDescription="lorem lipsum",
				MediaUrl= "http://dummyimage.com/178x146.png/cc0000/ffffff"
			};
			//post = _db.Posts.FirstOrDefault(x => x.PostId == request.PostId);
			//reply = _mapper.Map<PostReply>(post);
			
			Debug.WriteLine(reply.PostTitle);
			
			return Task.FromResult(reply);
		}
	}
}
