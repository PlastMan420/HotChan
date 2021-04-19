using AutoMapper;
using Grpc.Core;
using HotChanApi.Data;
using HotChanShared.Models;
using System.Linq;
using System.Threading.Tasks;

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
			var post = new Post();
			var reply = new PostReply();

			post = _db.Posts.FirstOrDefault(x => x.PostId == request.PostId);
			reply = _mapper.Map<PostReply>(post);

			// doing this conversion in the service instead of at entityframework. since this format is not
			// all that useful outside anything not gRPC related.
			reply.PostTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(post.Time.ToUniversalTime());

			return Task.FromResult(reply);
		}
	}
}