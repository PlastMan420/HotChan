using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace HotChanShared.Models
{
	public class PostDialogueDto
	{
		[Required]
		public string	UserId		{ get; set; }
		
		[Required]
		public string	PostTitle	{ get; set; }
		
		[Required]
		public List<Guid> Tags		{ get; set; }
		
		[Required]
		public IFormFile file { get; set; }

		public string Description { get; set; }

	}
}
