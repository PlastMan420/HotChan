using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotChanApi.Data;
using HotChanApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotChanApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChanController : ControllerBase
	{
		private readonly DataContext	_db; // --> naming convention. add underscore to private data.
		private readonly IThreadBox		_threadBox;
		private readonly IMapper		_mapper;

		public ChanController(DataContext context, IThreadBox threadbox, IMapper mapper)
		{
			_db = context;
			_threadBox = threadbox;
			_mapper = mapper;
		}

		[HttpGet("{getId}")]
		public async Task<Post> GetPostbyId(long getId)
		{
			return await _db.Posts.FirstOrDefaultAsync(x => x.Get == getId);
			
		}

		[HttpPost("new")]
		public async Task<IActionResult> AddPost([FromForm]PostDialogueDto newPostDialogueDto)
		{
			var newPost = _mapper.Map<Post>(newPostDialogueDto);
			var createdPost = await _threadBox.NewPost(newPost);
			return Ok(createdPost.Get);
		}
		
		[HttpPost("{getId}/reply")]
		public async Task<IActionResult> AddReply(Reply reply, long getId)
		{
			var newPost = new Reply
			{
				Name		= reply.Name,
				Flags		= reply.Flags,
				Comment		= reply.Comment,
				//MediaUrl	= reply.MediaUrl
			};
			var createdPost = await _threadBox.ReplyPost(getId, newPost);
			return Ok(createdPost.Get);
		}
	}
}
