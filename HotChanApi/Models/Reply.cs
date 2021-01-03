using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Models
{
	public class Reply
	{
		[Key]
		public long id { get; set; }
		public long parentPostId { get; set; }
		public string name { get; set; }
		public string comment { get; set; }
		public DateTime time { get; set; }
		public string mediaUrl { get; set; }
	}
}
