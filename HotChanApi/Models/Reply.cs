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
		public long get { get; set; }
		public string name { get; set; }
		public string options { get; set; }
		public string flags { get; set; }
		public string comment { get; set; }
		public DateTime time { get; set; }
		public string mediaUrl { get; set; }
	}
}
