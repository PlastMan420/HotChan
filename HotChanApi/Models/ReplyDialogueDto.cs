using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Models
{
	public class ReplyDialogueDto
	{
		[Required]
		public Guid PostId { get; set; }
		[Required]
		public Guid UserId { get; set; }
		[Required]
		public string Comment { get; set; }
	}
}
