using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotChanApi.Models
{
	public class Reply
	{
		[Key]
		public Guid		ReplyId				{ get; set; }
		public Guid		PostId				{ get; set; }
		public Guid		UserId				{ get; set; }
		public string	Comment				{ get; set; }
		public DateTime Time				{ get; set; }
		public Uri		AvatarThumbnailUrl	{ get; set; }
	}
}
