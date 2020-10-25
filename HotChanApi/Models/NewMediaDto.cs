using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Models
{
	public class NewMediaDto
	{
		public string Url { get; set; }
		public IFormFile File { get; set; }
	}
}
