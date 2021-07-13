using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotChan.DataBase.Models
{
	public class PostDialogueDto
	{
		[Required]
		public string	UserId		{ get; set; }
		
		[Required]
		public string	PostTitle	{ get; set; }
		
		[Required]
		public List<Guid> Tags		{ get; set; }

		public string Description { get; set; }

	}
}
