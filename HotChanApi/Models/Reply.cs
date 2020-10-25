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
		public long Get { get; set; }
		public long ParentPostGet { get; set; }
		public string Name { get; set; }
		public string Flags { get; set; }
		public string Comment { get; set; }
		public DateTime Time { get; set; }
		//public string mediaUrl { get; set; }
	}
}
