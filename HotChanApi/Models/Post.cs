using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HotChanApi.Models
{
	public class Post
	{
		[Key]
		public	Guid		PostId				{ get; set; }
		public	Guid		UserId				{ get; set; }
		public	string		PostTitle			{ get; set; }
		public	string		Tags				{ get; set; }
		public	string		Description			{ get; set; }
		public	DateTime	Time				{ get; set; }
		public	Uri			MediaUrl			{ get; set; }

		public string InternalReplyIds { get; set; }
		
		[NotMapped]
		public Guid[] ReplyIds
		{
			get
			{
				return Array.ConvertAll(InternalReplyIds.Split(';'), Guid.Parse);
			}
			set
			{
				InternalReplyIds = String.Join(";", value.Select(p => p.ToString()).ToArray());
			}
		}

	}
}
