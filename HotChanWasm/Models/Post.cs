using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HotChanWasm.Models
{
	public class Post
	{
		[Key]
		public	long		postId				{ get; set; }
		public	string		userId				{ get; set; }
		public	string		title				{ get; set; }
		public 	string		tags				{ get; set; }
		public	string		comment				{ get; set; }
		public	DateTime	time				{ get; set; }
		public	string		mediaUrl			{ get; set; }
		public 	string 		thumbnailMediaUrl 	{ get; set; }

		
	}
}
