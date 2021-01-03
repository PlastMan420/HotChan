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
		public	long		id				{ get; set; }
		public	string		name			{ get; set; }
		public	string		title			{ get; set; }
		public string		tags			{ get; set; }
		public	string		comment			{ get; set; }
		public	DateTime	time			{ get; set; }
		public	string		mediaUrl		{ get; set; }
		
	}
}
