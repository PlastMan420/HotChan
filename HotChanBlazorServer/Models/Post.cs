using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace HotChanBlazorServer.Models
{
	public class Post
	{
		[Key]
		public	long		Get				{ get; set; }
		public	string		Name			{ get; set; }
		[Required]
		public	string		Title			{ get; set; }
		public string		Tags			{ get; set; }
		public	string		Comment			{ get; set; }
		//public  string      Board			{ get; set; }
		public	DateTime	Time			{ get; set; }
		public	string		MediaUrl		{ get; set; }
		//public	int			Priority		{ get; set; }
		//public	bool		IsArchived		{ get; set; }
		//public 	bool		IsPruned		{ get; set; }
		
	}
}
