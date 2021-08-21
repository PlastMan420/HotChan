using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotChan.DataBase.Models
{
	public class PostDialogueDto
	{
		public Guid	UserId		{ get; set; }
		
		[Required]
		[MaxLength(20)]
		public string	PostTitle	{ get; set; }
		
		[Required]
		public string[] Tags		{ get; set; }

		[MaxLength(500)]
		public string Description { get; set; }

	}
}
