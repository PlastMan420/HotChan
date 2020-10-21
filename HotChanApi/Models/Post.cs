using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace HotChanApi.Models
{
	public class Post
	{
		[Key]
		public	long		get				{ get; set; }
		public	string		name			{ get; set; }
		public	string		options			{ get; set; }
		[Required]
		public	string		title			{ get; set; }
		public string		flags			{ get; set; }
		public	string		comment			{ get; set; }
		public  string      board			{ get; set; }
		public	DateTime	time			{ get; set; }
		public	string		mediaUrl		{ get; set; }
		public	int			priority		{ get; set; }
		public	bool		isArchived		{ get; set; }
		public 	bool		isPruned		{ get; set; }
		public string		subPosts		{ get; set; }
	}
}
