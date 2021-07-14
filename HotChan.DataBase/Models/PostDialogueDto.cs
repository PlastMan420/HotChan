using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotChan.DataBase.Models
{
	public class PostDialogueDto
	{
		[Required]
		public Guid	UserId		{ get; set; }
		
		[Required]
		[MaxLength(20)]
		public string	PostTitle	{ get; set; }
		
		[Required]
		public List<Guid> Tags		{ get; set; }

		[MaxLength(500)]
		public string Description { get; set; }

	}
}
