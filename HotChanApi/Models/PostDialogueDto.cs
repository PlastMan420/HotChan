using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Models
{
	public class PostDialogueDto
	{
		public string Board { get; set; }
		public string Name { get; set; }
		[Required]
		public string Title { get; set; }
		public string Flags { get; set; }
		public string Comment { get; set; }

		//public string mediaUrl { get; set; }
		[Required]
		public IFormFile File { get; set; }
	}
}
