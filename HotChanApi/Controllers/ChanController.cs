using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
		private readonly DataContext _db; // --> naming convention. add underscore to private data.
		private readonly IThreadBox _threadBox;

		public ChanController(DataContext context, IThreadBox threadbox)
		{
			_db = context;
			_threadBox = threadbox;
		}

		[HttpGet("{getId}")]
		public async Task<IActionResult> GetPostbyId(long getId)
		{
			if (await _db.Posts.FirstOrDefaultAsync(x => x.get == getId) != null)
				return Ok(getId);
			return NotFound();
		}

		[HttpPost("new")]
		public async Task<IActionResult> AddPost(PostDialogueDto newPostDialogueDto)
		{
			var newPost = new Post
			{
				board		= newPostDialogueDto.board,
				name		= newPostDialogueDto.name,
				title		= newPostDialogueDto.title,
				flags		= newPostDialogueDto.flags,
				comment		= newPostDialogueDto.comment,
				mediaUrl	= newPostDialogueDto.mediaUrl
			};
			var createdPost = await _threadBox.NewPost(newPost);
			return Ok(createdPost.get);
		}
		
		[HttpPost("{getId}/reply")]
		public async Task<IActionResult> AddReply(Reply reply, long getId)
		{
			var newPost = new Reply
			{
				name		= reply.name,
				flags		= reply.flags,
				comment		= reply.comment,
				mediaUrl	= reply.mediaUrl
			};
			var createdPost = await _threadBox.ReplyPost(getId, newPost);
			return Ok(createdPost.get);
		}
	}
}
