using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotChanApi.Data;
using HotChanApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HotChanApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChanController : ControllerBase
	{
		private readonly DataContext	_db; // --> naming convention. add underscore to private data.
		private readonly IThreadBox		_threadBox;
		private readonly IMapper		_mapper;
		private readonly IHostEnvironment _environment;
		enum allowedExtensions { png, apng, jpg, jpeg, bmp, avif, heic, gif, pnga, mp4, webm, mkv };
		public ChanController(DataContext context, IThreadBox threadbox, IMapper mapper, IHostEnvironment environment)
		{
			_db = context;
			_threadBox = threadbox;
			_mapper = mapper;
			_environment = environment;
		}

		[HttpGet("{getId}")]
		public async Task<Post> GetPostbyId(long id)
		{
			return await _db.Posts.FirstOrDefaultAsync(x => x.id == id);
			
		}

		[HttpPost("new")]
		public async Task<IActionResult> AddPost([FromForm]PostDialogueDto newPostDialogueDto)
		{
			var file = newPostDialogueDto.file;

			if (newPostDialogueDto.file.Length == 0) 
				return BadRequest("A file is required");

			string fileName = file.FileName;
			string fileExtension = Path.GetExtension(fileName);

			if (!Enum.IsDefined(typeof(allowedExtensions), fileExtension))
				return BadRequest("Not a valid media file");

			string postFileRepo = $"{Guid.NewGuid()}";
			string newFileName = $"{postFileRepo}{fileExtension}";
			string filePath = Path.Combine(_environment.ContentRootPath, $"media/{postFileRepo}", newFileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				await file.CopyToAsync(fileStream);
			}

			var newPost = _mapper.Map<Post>(newPostDialogueDto);
			newPost.mediaUrl = filePath;

			var createdPost = await _threadBox.NewPost(newPost);
			return Ok(createdPost.id); // use a router to navigate to "hotchan.com/board/{getid}"
		}
		
		[HttpPost("{getId}/reply")]
		public async Task<IActionResult> AddReply([FromForm]ReplyDialogueDto newReplyDialogueDto, long parentPostId)
		{
			var newReply = _mapper.Map<Reply>(newReplyDialogueDto);
			newReply.parentPostId = parentPostId;
			var createdPost = await _threadBox.ReplyPost(parentPostId, newReply);
			return Ok(createdPost.id);
		}
	}
}
