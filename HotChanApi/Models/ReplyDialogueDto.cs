using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Models
{
	public class ReplyDialogueDto
	{
		public long postId { get; set; }
		public string name { get; set; }
		public string comment { get; set; }
		public DateTime time { get; set; }
		public IFormFile file { get; set; }
	}
}
