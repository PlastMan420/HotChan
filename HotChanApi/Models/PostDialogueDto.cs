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
		public string name { get; set; }
		public string title { get; set; }
		public string tags { get; set; }
		public string comment { get; set; }

		[Required]
		public IFormFile file { get; set; }
	}
}
