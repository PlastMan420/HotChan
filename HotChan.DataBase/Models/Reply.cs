using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotChan.DataBase.Models
{
	[Index(nameof(ReplyId), IsUnique = true)]
	public class Reply
	{
		[Key]
		public Guid		ReplyId				{ get; set; }
		[Required]
		public Guid		PostId				{ get; set; }
		[Required]
		public Guid		UserId				{ get; set; }
		[Required]
		public string	Comment				{ get; set; }
		[Required]
		public DateTime Time				{ get; set; }
		[Required]
		public Uri		AvatarThumbnailUrl	{ get; set; }
	}
}
