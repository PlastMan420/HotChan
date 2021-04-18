using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

using HotChanApi.Data;
using HotChanShared.Models;
using HotChanApi.Filters;
using HotChanApi.Helpers;
using HotChanApi.Services;

namespace HotChanApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChanController : ControllerBase
	{
		private readonly DataContext	_db; // --> naming convention. add underscore to private data.
		private readonly IThreadBox		_threadBox;
		private readonly IMapper		_mapper;
		private readonly IWebHostEnvironment _environment;
		private readonly IUriService _uriService;

		enum allowedExtensions { png, apng, jpg, jpeg, bmp, avif, heic, gif, pnga, mp4, webm, mkv };

		public ChanController(DataContext context, IThreadBox threadbox, IMapper mapper, IWebHostEnvironment environment, IUriService uriService)
		{
			_db = context;
			_threadBox = threadbox;
			_mapper = mapper;
			_environment = environment;
			_uriService = uriService;

		}

		[HttpGet("{PostId}")]
		//public async Task<Post> GetPostbyId(long id)
		//{
		//	return await _db.Posts.FirstOrDefaultAsync(x => x.PostId == id);
			
		//}

		[HttpGet("catalog")]
		public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
		{
			var route = Request.Path.Value;
			var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
			var pagedData = await _db.Posts
				.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
				.Take(validFilter.PageSize)
				.ToListAsync();
			var totalRecords = await _db.Posts.CountAsync();
			var pagedReponse = PaginationHelper.CreatePagedReponse<Post>(pagedData, validFilter, totalRecords, _uriService, route);
    		return Ok(pagedReponse);
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

			string postFileRepo = $"{DateTime.Now.Ticks}";
			string newFileName = $"{postFileRepo}.{fileExtension}";
			string filePath = Path.Combine(_environment.ContentRootPath, $"media/{postFileRepo}", newFileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				await file.CopyToAsync(fileStream);
			}

			// Create a Post object out of the source PostDialogueDto object
			var newPost = _mapper.Map<Post>(newPostDialogueDto);
			
			///// PLACEHOLDER FUNCTIONALITY//////////////////////////
			//newPost.MediaUrl = filePath;
			/////////////////////////////////////////////////////////
			// give the new post the server's time.
			newPost.Time = DateTime.Now;

			var createdPost = await _threadBox.NewPost(newPost);
			return Ok(createdPost.PostId); // use a router to navigate to "hotchan.com/board/{getid}"
		}
		
		[HttpPost("{PostId}/reply")]
		public async Task<IActionResult> AddReply([FromForm]ReplyDialogueDto newReplyDialogueDto, long postId)
		{
			var newReply = _mapper.Map<Reply>(newReplyDialogueDto);
			//newReply.PostId = postId;
			var createdPost = await _threadBox.ReplyPost(postId, newReply);
			return Ok(createdPost.ReplyId);
		}
	}
}
