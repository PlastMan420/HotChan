using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace HotChanShared.Models
{
	public class ReplyDialogueDto
	{
		[Required]
		public ulong PostId { get; set; }
		[Required]
		public ulong UserId { get; set; }
		[Required]
		public string Comment { get; set; }
	}
}
