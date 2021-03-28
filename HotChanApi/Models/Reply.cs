using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Models
{
	public class Reply
	{
		[Key]
		public long		ReplyId { get; set; }
		public string	Name { get; set; }
		public string	Comment { get; set; }
		public DateTime Time { get; set; }
		public string	MediaUrl { get; set; }
		public string	MediaThumbnailUrl { get; set; }
		public long		PostId { get; set; }
	}
}
