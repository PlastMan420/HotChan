using Microsoft.EntityFrameworkCore;
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
		[Column(TypeName = "bigint")]
		public ulong PostId { get; set; }

		[Required]
		[Column(TypeName = "bigint")]
		public ulong UserId { get; set; }

		[Required]
		[MaxLength(50)]
		public string PostTitle { get; set; }
		[Required]
		public string Tags { get; set; }
		public string Description { get; set; }
		[Required]
		public DateTime Time { get; set; }
		[Required]
		public Uri MediaUrl { get; set; }

	}
}
