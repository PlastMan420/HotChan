using System;
using System.ComponentModel.DataAnnotations;

namespace HotChan.DataBase.Models.Dtos;

public class PostDialogueDto
{
	public Guid	UserId { get; set; }

	[Required]
	[MaxLength(20)]
	public string PostTitle { get; set; }

	[Required]
	public string[] Tags{ get; set; }

	[MaxLength(500)]
	public string Description { get; set; }

	public Uri MediaUrl { get; set; }
}

