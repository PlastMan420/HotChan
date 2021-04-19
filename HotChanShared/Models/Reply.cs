using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotChanShared.Models
{
	[Index(nameof(ReplyId), IsUnique = true)]
	public class Reply
	{
		[Key]
		public ulong ReplyId				{ get; set; }
		[Required]
		public ulong PostId				{ get; set; }
		[Required]
		public ulong UserId				{ get; set; }
		[Required]
		public string	Comment				{ get; set; }
		[Required]
		public DateTime Time				{ get; set; }
		[Required]
		public Uri		AvatarThumbnailUrl	{ get; set; }
	}
}
