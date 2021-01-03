using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace HotChanApi.Models
{
	public class Post
	{
		[Key]
		public	long		Get				{ get; set; }
		public	string		Name			{ get; set; }
		public	string		Title			{ get; set; }
		public string		Tags			{ get; set; }
		public	string		Comment			{ get; set; }
		public	DateTime	Time			{ get; set; }
		public	string		MediaUrl		{ get; set; }
		
	}
}
