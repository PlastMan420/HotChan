﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanWasm.Models
{
	public class ViewPostDto
	{
		public long Get { get; set; }
		public string name { get; set; }
		public string title { get; set; }
		public string comment { get; set; }
		public DateTime Time { get; set; }


	}
}