using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HotChan.DataBase.Models
{
	public class Post
	{
		[Key]
		public Guid PostId { get; set; }

		[MaxLength(20)]
		public string PostTitle { get; set; }

		public string Tags { get; set; }
		public string Description { get; set; }

		[Required]
		public DateTimeOffset Time { get; set; }

		[Required]
		public Uri MediaUrl { get; set; }

		[ForeignKey("User")]
		public Guid UserId { get; set; }
		public User User { get; set; }

	}
}
