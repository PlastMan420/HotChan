using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Models
{
	public class ReplyDialogueDto
	{
		public long ParentPostGet { get; set; }
		public string Name { get; set; }
		public string Flags { get; set; }
		public string Comment { get; set; }
		public DateTime Time { get; set; }
		public IFormFile File { get; set; }
	}
}
