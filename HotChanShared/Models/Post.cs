using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotChanShared.Models
{
	public class Post
	{
		[Key]
		[Column(TypeName = "bigint")]
		public ulong PostId { get; set; }

		[Required]
		[Column(TypeName = "bigint")]
		public ulong UserId { get; set; }

		[Required]
		[MaxLength(20)]
		public string PostTitle { get; set; }

		public string Tags { get; set; }
		public string Description { get; set; }

		[Required]
		public DateTime Time { get; set; }

		[Required]
		public Uri MediaUrl { get; set; }

		//public string InternalReplyIds	{ get; set; }
	}
}